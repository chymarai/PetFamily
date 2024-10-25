using CSharpFunctionalExtensions;
using Microsoft.Win32.SafeHandles;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetsManagment.Entities;

public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;

    private Pet(PetId id) : base(id)
    {

    }

    public Pet(
        PetId petId,
        Name name,
        Description description,
        SpeciesBreed speciesBreed,
        Color color,
        HealthInformation healthInformation,
        Address address,
        Weight weight,
        Height height,
        PhoneNumber phoneNumber,
        bool isCastrated,
        bool isVaccination,
        AssistanceStatus assistanceStatus,
        DateTime birthDate,
        DateTime dateOfCreation,
        RequisiteDetails requisiteDetails,
        ValueObjectList<PetFiles> files 
        ) : base(petId)
    {
        Name = name;
        Description = description;
        SpeciesBreed = speciesBreed;
        Color = color;
        HealthInformation = healthInformation;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsCastrated = isCastrated;
        IsVaccination = isVaccination;
        AssistanceStatus = assistanceStatus;
        BirthDate = birthDate;
        DateOfCreation = dateOfCreation;
        RequisiteDetails = requisiteDetails;
        Files = files;
    }

    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public SpeciesBreed SpeciesBreed { get; private set; } = default!;
    public Color Color { get; private set; } = default!;
    public HealthInformation HealthInformation { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public Weight Weight { get; private set; } = default!;
    public Height Height { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public bool IsCastrated { get; private set; } = default!;
    public bool IsVaccination { get; private set; }
    public AssistanceStatus AssistanceStatus { get; private set; } = default!;
    public DateTime BirthDate { get; private set; } = default!;
    public DateTime DateOfCreation { get; private set; } = default!;
    public RequisiteDetails RequisiteDetails { get; private set; }
    public ValueObjectList<PetFiles> Files { get; private set; }

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;

    public void UpdateFilesList(ValueObjectList<PetFiles> files) =>
        Files = files;
}
