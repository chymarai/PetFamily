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

namespace PetFamily.Application.Volunteers.SaveMainInfo;
public class SaveMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<SaveMainInfoHandler> _logger;

    public SaveMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<SaveMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        SaveMainInfoRequest request,
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

        volunteerResult.Value.SaveMainInfo(fullName, email, phoneNumber, description, experience);

        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        return result;
    }


}
