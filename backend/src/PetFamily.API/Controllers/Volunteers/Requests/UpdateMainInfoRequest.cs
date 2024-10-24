using PetFamily.Application.DTOs;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.API.Controllers.Volunteers.Contracts;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience)
{
    public UpdateMainInfoCommand ToCommand(Guid VolunteerId) =>
        new(VolunteerId, FullName, Email, PhoneNumber, Description, Experience);
}

