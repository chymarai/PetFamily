using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoRequest(
    Guid VolunteerId,
    UpdateMainInfoDto Dto);

public record UpdateMainInfoDto(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    RequisiteDetailsDto RequisiteDetails);

