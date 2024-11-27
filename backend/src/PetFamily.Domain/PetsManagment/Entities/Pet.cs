using CSharpFunctionalExtensions;
using Microsoft.Win32.SafeHandles;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.Aggregate;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetFamily.Domain.PetsManagment.Entities;

public class Pet : SoftDeletableEntity<PetId>
{
    private readonly List<PetFiles> _files = [];

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
        BirthDate birthDate,
        DateTime dateOfCreation,
        RequisiteDetails requisiteDetails,
        ValueObjectList<PetFiles>? files 
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
        _files = files ?? new ValueObjectList<PetFiles>([]);
    }

    public Name Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Position Position { get; private set; } = default!;
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
    public BirthDate BirthDate { get; private set; } = default!;
    public DateTime DateOfCreation { get; private set; } = default!;
    public RequisiteDetails RequisiteDetails { get; private set; }
    public IReadOnlyList<PetFiles> Files => _files;

    public void UpdateFiles(ValueObjectList<PetFiles> files) =>
        _files.AddRange(files);

    public void RemoveFiles(PetFiles files) =>
        _files.Remove(files);

    public void SetPosition(Position position) =>
        Position = position;

    public void UpdateAssistanceStatus(AssistanceStatus assistanceStatus)
    {
        AssistanceStatus = assistanceStatus;
    }

    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }
}
