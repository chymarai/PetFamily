using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;

namespace PetFamily.Volunteers.Application.Commands.UpdateMainInfos;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience) : ICommand;

