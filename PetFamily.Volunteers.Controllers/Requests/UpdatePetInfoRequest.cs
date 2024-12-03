using PetFamily.Core.DTOs;
using PetFamily.Volunteers.Application.Commands.UpdatePetInfo;

namespace PetFamily.Volunteers.Presentation.Requests;

public record UpdatePetInfoRequest(
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
    RequisiteDetailsDto RequisiteDetails)
{
    public UpdatePetInfoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId,
            petId,
            Name,
            Description,
            SpeciesBreed,
            Color,
            HealthInformation,
            Address,
            Weight,
            Height,
            PhoneNumber,
            IsCastrated,
            IsVaccination,
            AssistanceStatus,
            BirthDate,
            DateOfCreation,
            RequisiteDetails);
}
