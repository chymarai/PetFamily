using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Specieses.Contracts;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Application.Commands.AddPet;
public class CreatePetHandler : ICommandHandler<Guid, CreatePetCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetCommand> _validator;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IWriteVolunteersRepository volunteersRepository,
        ISpeciesContract speciesContract,
        IUnitOfWork unitOfWork,
        IValidator<CreatePetCommand> validator,
        ILogger<CreatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
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
        var speciesResult = _speciesContract.SpeciesExist(speciesId, token);

        var breedId = BreedId.Create(command.SpeciesBreed.Id).Value;
        var breedResult = _speciesContract.BreedExists(breedId, token);

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

        var assistanceStatus = Enum.Parse<AssistanceStatus>(command.AssistanceStatus, true);

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
