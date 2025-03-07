using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using PetFamily.Volunteers.Application.Commands.AddFiles;
using PetFamily.Volunteers.Application.Commands.AddPet;
using PetFamily.Volunteers.Application.Commands.Create;
using PetFamily.Volunteers.Application.Commands.DeletePetFile;
using PetFamily.Volunteers.Application.Commands.DeleteVolunteer;
using PetFamily.Volunteers.Application.Commands.HardDeletePet;
using PetFamily.Volunteers.Application.Commands.SoftDeletePet;
using PetFamily.Volunteers.Application.Commands.UpdateMainInfos;
using PetFamily.Volunteers.Application.Commands.UpdatePetAssistanceStatus;
using PetFamily.Volunteers.Application.Commands.UpdatePetInfo;
using PetFamily.Volunteers.Application.Commands.UpdateSocialNetwork;
using PetFamily.Volunteers.Application.Queries.GetPetById;
using PetFamily.Volunteers.Application.Queries.GetPetsWithPaginationAndFiltering;
using PetFamily.Volunteers.Application.Queries.GetVolunteerById;
using PetFamily.Volunteers.Application.Queries.GetVolunteersWithPagination;
using PetFamily.Volunteers.Presentation.Requests;
using PetFamily.Web.Prosessors;

namespace PetFamily.Volunteers.Presentation;

public class VolunteersController : ApplicationController
{
    [Permission(Permissions.Volunteers.GetVolunteer)]
    [HttpGet]
    public async Task<ActionResult> GetVolunteers(
        [FromServices] GetVolunteersWithPaginationHandler handler,
        [FromQuery] GetVolunteersWithPaginationRequest request,
        CancellationToken token = default)
    {
        var result = await handler.Handle(request.ToCommand(), token);

        return Ok(result);

    }

    [Permission(Permissions.Volunteers.GetVolunteer)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetVolunteerById(
        [FromServices] GetVolunteerByIdHandler handler,
        [FromRoute] Guid id,
        CancellationToken token = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.Handle(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.GetPet)]
    [HttpGet("/pet")]
    public async Task<ActionResult> GetPets(
       [FromServices] GetPetsWithPaginationAndFilteringHandler handler,
       [FromQuery] GetPetsWithPaginationAndFilteringRequest request,
       CancellationToken token = default)
    {
        var result = await handler.Handle(request.ToCommand(), token);

        return Ok(result);
    }

    [Permission(Permissions.Pets.GetPet)]
    [HttpGet("/pet/{id:guid}")]
    public async Task<ActionResult> GetPetById(
      [FromServices] GetPetByIdHandler handler,
      [FromRoute] Guid id,
      CancellationToken token = default)
    {
        var query = new GetPetByIdQuery(id);

        var result = await handler.Handle(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Volunteers.CreateVolunteer)]
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Volunteers.UpdateVolunteer)]
    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    //[HttpPut("{id:guid}/social-network")]
    //public async Task<ActionResult> Create(
    //    [FromRoute] Guid id,
    //    [FromServices] UpdateSocialNetworkHandler handler,
    //    [FromBody] UpdateSocialNetworkRequest request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await handler.Handle(request.ToCommand(id), cancellationToken);

    //    if (result.IsFailure)
    //        return result.Error.ToResponse();

    //    return Ok(result.Value);
    //}

    [Permission(Permissions.Volunteers.DeleteVolunteer)]
    [HttpDelete("{id:guid}/delete")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken token = default)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.CreatePet)]
    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> CreatePet(
        [FromRoute] Guid id,
        [FromBody] CreatePetRequest request,
        [FromServices] CreatePetHandler handler,
         CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.UpdateFile)]
    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/files")]
    public async Task<ActionResult> AddFilesForPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFilesToPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProsessor = new FormFileProsessor();

        var fileDtos = fileProsessor.Process(files);

        var command = new UploadFilesToPetCommand(volunteerId, petId, fileDtos);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.DeleteFile)]
    [HttpDelete("{volunteerId:guid}/pet/{PetId:guid}/file-delete")]
    public async Task<ActionResult> FileDelete(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetFileHandler handler,
        [FromBody] RemovePetFilesRequest request,
        CancellationToken token = default)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.UpdatePet)]
    [HttpPut("{volunteerId:guid}/pet/{PetId:guid}/info")]
    public async Task<ActionResult> UpdatePetInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetInfoHandler handler,
        [FromBody] UpdatePetInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(volunteerId, petId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.UpdatePet)]
    [HttpPut("{volunteerId:guid}/pet/{PetId:guid}/assistance-status")]
    public async Task<ActionResult> UpdatePetAssistanceStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetAssistanceStatusHandler handler,
        [FromBody] UpdatePetAssistanceStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(volunteerId, petId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.SoftDeletePet)]
    [HttpDelete("{volunteerId:guid}/pet/{PetId:guid}/soft-delete")]
    public async Task<ActionResult> SoftDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken token = default)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.HardDeletePet)]
    [HttpDelete("{volunteerId:guid}/pet/{PetId:guid}/hard-delete")]
    public async Task<ActionResult> HardDeletePet(
    [FromRoute] Guid volunteerId,
    [FromRoute] Guid petId,
    [FromServices] HardDeletePetHandler handler,
    CancellationToken token = default)
    {
        var command = new HardDeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
