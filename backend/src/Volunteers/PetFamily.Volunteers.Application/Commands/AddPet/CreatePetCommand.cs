using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;

namespace PetFamily.Volunteers.Application.Commands.AddPet;
public record CreatePetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    SpeciesBreedDto SpeciesBreed,
    string Color,
    string HealthInformation,
    AddressDto Address,
    int Weight,
    int Height,
    string PhoneNumber,
    bool IsCastrated,
    bool IsVaccination,
    string AssistanceStatus,
    DateTime BirthDate,
    DateTime DateOfCreation,
    RequisiteDetailsDto RequisiteDetails) : ICommand;