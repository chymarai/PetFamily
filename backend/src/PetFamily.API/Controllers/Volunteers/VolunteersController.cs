using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers;
using PetFamily.API.Controllers.Volunteers.Contracts;
using PetFamily.API.Extensions;
using PetFamily.API.Prosessors;
using PetFamily.API.Response;
using PetFamily.Application.DTOs;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Pet.Create;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.DeleteVolunteer;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateSocialNetwork;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace PetFamily.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //вызов бизнес логики
        var command = new CreateVolunteerCommand(
            request.FullName,
            request.Email,
            request.PhoneNumber,
            request.Description,
            request.Experience,
            request.SocialNetworkDetails,
            request.RequisiteDetails);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateMainInfoCommand(
            id,
            request.FullName,
            request.Email,
            request.PhoneNumber,
            request.Description,
            request.Experience,
            request.RequisiteDetails
            );

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-network")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialNetworkHandler handler,
        [FromBody] UpdateSocialNetworkDto dto,
        [FromServices] IValidator<UpdateSocialNetworkCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateSocialNetworkCommand(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
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
    [FromForm] CreatePetRequest request,
    [FromServices] CreatePetHandler handler,
    CancellationToken cancellationToken = default)
    {
        await using var fileProsessor = new FormFileProsessor();

        var filesDto = fileProsessor.Process(request.Files);

        var command = new CreatePetCommand(
             id,
             request.Name,
             request.Description,
             request.SpeciesBreed,
             request.Color,
             request.HealthInfornmation,
             request.Address,
             request.Weight,
             request.Height,
             request.PhoneNumber,
             request.IsCastrated,
             request.IsVaccination,
             request.AssistanceStatus,
             request.BirthDate,
             request.DateOfCreation,
             request.RequisiteDetails,
             filesDto);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}
