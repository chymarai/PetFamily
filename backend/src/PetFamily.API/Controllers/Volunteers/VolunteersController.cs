using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteers.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Prosessors;
using PetFamily.Application.PetsManagment.Commands.UpdatePetInfo;
using PetFamily.Application.PetsManagment.Queries.GetVolunteerById;
using PetFamily.Application.Volunteers.Commands.AddFiles;
using PetFamily.Application.Volunteers.Commands.AddPet;
using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PetFamily.Application.Volunteers.WriteHandler.Create;
using PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
using PetFamily.Application.Volunteers.WriteHandler.UpdateMainInfos;
using PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;

namespace PetFamily.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> GetVolunteers(
        [FromServices]GetVolunteersWithPaginationHandler handler,
        [FromQuery]GetVolunteersWithPaginationRequest request,
        CancellationToken token = default)
    {
        var result = await handler.Handle(request.ToCommand(), token);

        return Ok(result);

    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetVolunteerById(
        [FromServices]GetVolunteerByIdHandler handler,
        [FromRoute]Guid id,
        CancellationToken token = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.Handle(query, token);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

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

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> Create(
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

    [HttpPut("{id:guid}/social-network")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialNetworkHandler handler,
        [FromBody] UpdateSocialNetworkRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

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

    [HttpPost("{VolunteerId:guid}/pet/{petId:guid}/files")]
    public async Task<ActionResult> AddFilesForPet(
    [FromRoute] Guid VolunteerId,
    [FromRoute] Guid petId,
    [FromForm] IFormFileCollection files,
    [FromServices] UploadFilesToPetHandler handler,
    CancellationToken cancellationToken = default)
    {
        await using var fileProsessor = new FormFileProsessor();

        var fileDtos = fileProsessor.Process(files);

        var command = new UploadFilesToPetCommand(VolunteerId, petId, fileDtos);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{VolunteerId:guid}/pet/{PetId:guid}/info")]
    public async Task<ActionResult> Create(
    [FromRoute] Guid VolunteerId,
    [FromRoute] Guid PetId,
    [FromServices] UpdatePetInfoHandler handler,
    [FromBody] UpdatePetInfoRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(VolunteerId, PetId), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
