﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Domain;

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

    public void UpdatePetInfo(
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
        RequisiteDetails requisiteDetails)
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
    }

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

    public void DeleteFiles(string[] photoPaths)
    {
        bool primaryPhotoWasDeleted = false;
        bool deletedAtLeastOnePhoto = false;

        foreach (var path in photoPaths)
        {
            int photoToDeleteIdx = _files.FindIndex(x => Equals(path, x.PathToStorage));
            if (photoToDeleteIdx == -1)
                continue;

            var photoToDelete = _files[photoToDeleteIdx];

            if (photoToDelete.IsMain)
                primaryPhotoWasDeleted = true;

            _files.RemoveAt(photoToDeleteIdx);
            deletedAtLeastOnePhoto = true;
        }

        if (deletedAtLeastOnePhoto == false)
            return;

        if (primaryPhotoWasDeleted && _files.Count > 0)
        {
            var firstPhotoMadePrimary = new PetFiles(_files[0].PathToStorage);
            _files[0] = firstPhotoMadePrimary;
        }

        var deepCopyData = _files.Select(x => new PetFiles(x.PathToStorage));
    }
}
