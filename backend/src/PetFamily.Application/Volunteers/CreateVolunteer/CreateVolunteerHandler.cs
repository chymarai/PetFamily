using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Infrastructure.Repositories;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _looger;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerRequest> validator,
        ILogger<CreateVolunteerHandler> looger)
    {
        _volunteersRepository = volunteersRepository;
        _looger = looger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var fullNameDto = request.FullName;
        var fullName = FullName.Create(fullNameDto.LastName, fullNameDto.FirstName, fullNameDto.Surname).Value;

        var email = Email.Create(request.Email).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        var description = Description.Create(request.Description).Value;
        var experience = Experience.Create(request.Experience).Value;

        var socialNetworkDetailsDto = request.SocialNetworkDetails;
        var socialNetworkDetails = SocialNetworkDetails.Create(socialNetworkDetailsDto.SocialNetwork
            .Select(s => SocialNetwork.Create(s.Name, s.Url).Value));

        var requisiteDetailsDto = request.RequisiteDetails;
        var requisiteDetails = RequisiteDetails.Create(requisiteDetailsDto.Requisite
            .Select(r => Requisite.Create(r.Name, r.Description).Value));


        var volunteer = await _volunteersRepository.GetByEmail(email);

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = Volunteer.Create(
            volunteerId,
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            socialNetworkDetails,
            requisiteDetails);

        await _volunteersRepository.Add(volunteerToCreate.Value, cancellationToken);

        _looger.LogInformation("Create volunteer {fullName} with id {volunteerId}", fullName, volunteerId);

        return volunteerId.Value;
    }
}

