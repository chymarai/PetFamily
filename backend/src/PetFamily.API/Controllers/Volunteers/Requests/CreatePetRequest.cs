using PetFamily.Application.DTOs;

namespace PetFamily.API.Controllers.Volunteers.Contracts;

public record CreatePetRequest(
    string Name,
    string Description,
    SpeciesBreedDto SpeciesBreed,
    string Color,
    string HealthInfornmation,
    AddressDto Address,
    int Weight,
    int Height,
    string PhoneNumber,
    bool IsCastrated,
    bool IsVaccination,
    string AssistanceStatus,
    DateOnly BirthDate,
    DateTime DateOfCreation,
    RequisiteDetailsDto RequisiteDetails,
    IFormFileCollection Files);
