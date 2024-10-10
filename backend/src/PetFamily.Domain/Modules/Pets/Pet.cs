using CSharpFunctionalExtensions;
using Microsoft.Win32.SafeHandles;
using PetFamily.Domain.Modules.Pets;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public class Pet : Shared.Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {

    }

    private Pet(PetId petId, string name, string description) : base(petId)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; } = default!;
    public SpeciesBreed SpeciesBreed { get; private set; } = default!; 
    public string Description { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInformation { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public int Weight { get; private set; } = default!;
    public int Height { get; private set; } = default!;
    public int PhoneNumber { get; private set; } = default!;
    public bool IsCastrated { get; private set; } = default!;
    public bool IsVaccination { get; private set; }
    public AssistanceStatus AssistanceStatus { get; private set; } = default!;
    public DateOnly BirthDate { get; private set; } = default!;
    public DateTime DateOfCreation { get; private set; } = default!;
    public RequisiteDetails RequisiteDetails { get; private set; }
    public Gallery Gallery { get; private set; }

    public static Result<Pet, Error> Create(PetId petId, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        return new Pet(petId, name, description);
    }
}
