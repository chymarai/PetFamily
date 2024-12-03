using PetFamily.Core.DTOs;
using PetFamily.Volunteers.Application.Commands.UpdateMainInfos;

namespace PetFamily.Volunteers.Presentation.Requests;

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

