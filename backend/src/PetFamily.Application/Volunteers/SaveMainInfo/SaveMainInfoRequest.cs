using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;
namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record SaveMainInfoRequest(
    Guid VolunteerId,
    SaveMainInfoDto Dto);

public record SaveMainInfoDto(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience);

