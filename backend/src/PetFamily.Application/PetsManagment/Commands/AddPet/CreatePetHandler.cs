using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
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
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Application.Abstraction;
using PetFamily.Application.PetsManagment.Queries;

namespace PetFamily.Application.Volunteers.Commands.AddPet;
public class CreatePetHandler : ICommandHandler<Guid, CreatePetCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetCommand> _validator;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IWriteVolunteersRepository volunteersRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        IValidator<CreatePetCommand> validator,
        ILogger<CreatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(CreatePetCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var speciesId = SpeciesId.Create(command.SpeciesBreed.SpeciesId).Value;

        var speciesResult = _readDbContext.Breeds.FirstOrDefault(s => s.SpeciesId == speciesId);
        if (speciesResult is null)
            return Errors.General.NotFound(speciesId).ToErrorList();

        var breedId = BreedId.Create(command.SpeciesBreed.BreedId).Value;

        var breedResult = _readDbContext.Breeds.FirstOrDefault(s => s.Id == breedId);
        if (breedResult is null)
            return Errors.General.NotFound(breedId).ToErrorList();

        var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;


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

        var assistanceStatus = Enum.Parse<AssistanceStatus>(command.AssistanceStatus, true); ;

        var birthdate = BirthDate.Create(command.BirthDate).Value;

        var requisite = RequisiteDetails.Create(command.RequisiteDetails.Requisite
           .Select(r => Requisite.Create(r.Name, r.Description).Value));

        var petId = PetId.NewPetId();

        var pet = new Pet(
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
