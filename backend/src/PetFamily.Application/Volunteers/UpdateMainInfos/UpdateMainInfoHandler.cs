using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;
public class UpdateMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteersRepository.GetById(VolunteerId.Create(request.VolunteerId), cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound();

        var fullNameDto = request.Dto.FullName;
        var fullName = FullName.Create(fullNameDto.LastName, fullNameDto.FirstName, fullNameDto.Surname).Value;

        var email = Email.Create(request.Dto.Email).Value;
        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;
        var description = Description.Create(request.Dto.Description).Value;
        var experience = Experience.Create(request.Dto.Experience).Value;

        var requisiteDetailsDto = request.Dto.RequisiteDetails;
        var requisiteDetails = RequisiteDetails.Create(requisiteDetailsDto.Requisite
            .Select(r => Requisite.Create(r.Name, r.Description).Value));

        volunteerResult.Value.UpdateMainInfo(fullName, email, phoneNumber, description, experience, requisiteDetails);

        var result = await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);

        _logger.LogInformation(
            "Update volunteer {fullName}, {email}, {phoneNumber}, {description}, {experience}, {requisit}",
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            requisiteDetails);

        return result;
    }


}
