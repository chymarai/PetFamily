using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Pet.Create;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using System.Transactions;
using System.Data.Common;
using PetFamily.Application.Specieses;
using FluentValidation;
using PetFamily.Application.Extensions;

namespace PetFamily.Application.PetCreate.Create;
public class CreatePetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetCommand> _validator;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreatePetCommand> validator,
        ILogger<CreatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(CreatePetCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;

        var speciesId = SpeciesId.NewSpeciesId().Value;
        var breedId = BreedId.NewBreedId().Value;
        var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var color = Color.Create(command.Color).Value;
        var healthInformation = HealthInformation.Create(command.HealthInformation).Value;

        var address = Address.Create(
            command.Address.Country,
            command.Address.Region,
            command.Address.City,
            command.Address.Street).Value;

        var weight = Weight.Create(command.Weight).Value;
        var height = Height.Create(command.Height).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var assistanceStatus = AssistanceStatus.AtHome;

        var birthdate = BirthDate.Create(command.BirthDate).Value;

        var requisite = RequisiteDetails.Create(command.RequisiteDetails.Requisite
           .Select(r => Requisite.Create(r.Name, r.Description).Value));

        var petId = PetId.NewPetId();

        var pet = new Domain.PetsManagment.Entities.Pet(
            petId,
            name,
            description,
            speciesBreed,
            color,
            healthInformation,
            address,
            weight,
            height,
            phoneNumber,
            command.IsCastrated,
            command.IsVaccination,
            assistanceStatus,
            birthdate,
            command.DateOfCreation,
            requisite,
            null);

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChanges(token);

        return pet.Id.Value;
    }
}
