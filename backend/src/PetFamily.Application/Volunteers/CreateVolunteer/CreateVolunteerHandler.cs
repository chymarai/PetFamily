using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;

        var descriptionResult = Description.Create(request.Description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var fullNameDto = request.FullName;

        var fullName = FullName.Create(fullNameDto.LastName, fullNameDto.FirstName, fullNameDto.Surname);

        var volunteer = await _volunteersRepository.GetByEmail(emailResult.Value);

        if (volunteer.IsSuccess)
            return Errors.Volunteer.AlreadyExist();

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = Volunteer.Create(volunteerId, fullName.Value, emailResult.Value, phoneNumberResult.Value, descriptionResult.Value);

        await _volunteersRepository.Add(volunteerToCreate.Value, cancellationToken);

        return volunteerId.Value;
    }
}

