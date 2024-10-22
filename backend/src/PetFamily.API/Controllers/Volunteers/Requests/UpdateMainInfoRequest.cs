using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.API.Controllers.Volunteers.Contracts;

public record UpdateMainInfoRequest(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    RequisiteDetailsDto RequisiteDetails);

