using PetFamily.Application.DTOs;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Pet.Create;
public record CreatePetCommand(
    Guid VolunteerId,
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
    IEnumerable<CreateFileCommand> Files
    );

public record CreateFileCommand(Stream Stream, string FileName);