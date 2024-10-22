using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    RequisiteDetailsDto RequisiteDetails);

