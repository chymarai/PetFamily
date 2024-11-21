using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.Application.Volunteers.WriteHandler.UpdateMainInfos;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience) : ICommand;

