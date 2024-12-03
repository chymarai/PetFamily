using PetFamily.Core.DTOs;
using PetFamily.Volunteers.Application.Commands.AddPet;

namespace PetFamily.Volunteers.Presentation.Requests;

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
    DateTime BirthDate,
    DateTime DateOfCreation,
    RequisiteDetailsDto RequisiteDetails)
{
    public CreatePetCommand ToCommand(Guid volunteerd) =>
        new(volunteerd, Name, Description, SpeciesBreed, Color, HealthInfornmation, Address, Weight, Height, PhoneNumber, IsCastrated, IsVaccination,
            AssistanceStatus, BirthDate, DateOfCreation, RequisiteDetails);
}
