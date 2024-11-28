using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.UpdatePetInfo;
public record UpdatePetInfoCommand(
    Guid VolunteerId,
    Guid PetId,
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

