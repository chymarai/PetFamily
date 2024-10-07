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

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<CreateVolunteerRequest> validator)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        var description = Description.Create(request.Description).Value;
        var fullNameDto = request.FullName;
        var fullName = FullName.Create(fullNameDto.LastName, fullNameDto.FirstName, fullNameDto.Surname).Value;

        var volunteer = await _volunteersRepository.GetByEmail(email);

        var volunteerId = VolunteerId.NewVolunteerId();

        var volunteerToCreate = Volunteer.Create(volunteerId, fullName, email, phoneNumber, description);

        await _volunteersRepository.Add(volunteerToCreate.Value, cancellationToken);

        return volunteerId.Value;
    }
}

