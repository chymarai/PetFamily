using PetFamily.Application.DTOs;
using PetFamily.Application.Pet.Create;

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
    DateTime BirthDate,
    DateTime DateOfCreation,
    RequisiteDetailsDto RequisiteDetails,
    IFormFileCollection Files)
{
    public CreatePetCommand ToCommand(Guid id, IEnumerable<CreateFileCommand> Files) =>
        new(id, Name, Description, SpeciesBreed, Color, HealthInfornmation, Address, Weight, Height, PhoneNumber, IsCastrated, IsVaccination, 
            AssistanceStatus, BirthDate, DateOfCreation, RequisiteDetails, Files);
}
