using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.PetsManagment.Aggregate;
using PetFamily.Application.Extensions;
using PetFamily.Application.Database;
using PetFamily.Application.Abstraction;
using PetFamily.Application.PetsManagment.Queries;

namespace PetFamily.Application.Volunteers.WriteHandler.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IWriteVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> looger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = looger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var fullNameDto = command.FullName;
        var fullName = FullName.Create(fullNameDto.LastName, fullNameDto.FirstName, fullNameDto.Surname).Value;

        var email = Email.Create(command.Email).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var description = Description.Create(command.Description).Value;
        var experience = Experience.Create(command.Experience).Value;

        var socialNetworkDetailsDto = command.SocialNetworkDetails;
        var socialNetworkDetails = SocialNetworkDetails.Create(socialNetworkDetailsDto.SocialNetwork
            .Select(s => SocialNetwork.Create(s.Name, s.Url).Value));

        var requisiteDetailsDto = command.RequisiteDetails;
        var requisiteDetails = RequisiteDetails.Create(requisiteDetailsDto.Requisite
            .Select(r => Requisite.Create(r.Name, r.Description).Value));

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = new Volunteer(
            volunteerId,
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            socialNetworkDetails,
            requisiteDetails);

        await _volunteersRepository.Add(volunteerToCreate, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Create volunteer {fullName} with id {volunteerId}", fullName, volunteerId);

        return volunteerId.Value;
    }
}

