using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Specieses.Contracts;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetInfo;
public class UpdatePetInfoHandler : ICommandHandler<Guid, UpdatePetInfoCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetInfoCommand> _validator;
    private readonly ILogger<UpdatePetInfoHandler> _logger;

    public UpdatePetInfoHandler(
        IWriteVolunteersRepository volunteersRepository,
        ISpeciesContract speciesContract,
        IUnitOfWork unitOfWork,
        IValidator<UpdatePetInfoCommand> validator,
        ILogger<UpdatePetInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(UpdatePetInfoCommand command, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(command, token);
        if (validationResult.IsValid == false)
            validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId).Value;

        var speciesId = SpeciesId.Create(command.SpeciesBreed.SpeciesId).Value;

        var speciesResult = _speciesContract.SpeciesExist(speciesId, token);
        if (speciesResult is null)
            return Errors.General.NotFound(speciesId).ToErrorList();

        var breedId = BreedId.Create(command.SpeciesBreed.Id).Value;

        var breedResult = _speciesContract.BreedExists(breedId, token);
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

        var assistanceStatus = Enum.Parse<AssistanceStatus>(command.AssistanceStatus, true);

        var birthdate = BirthDate.Create(command.BirthDate).Value;

        var requisite = RequisiteDetails.Create(command.RequisiteDetails.Requisite
           .Select(r => Requisite.Create(r.Name, r.Description).Value));

        petResult.UpdatePetInfo(
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
            requisite);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Update info with id {petId}", petResult.Id);

        return petResult.Id.Value;
    }
}
