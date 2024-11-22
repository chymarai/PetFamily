using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.SpeciesManagment;
using System.Net;
using System.Xml.Linq;

namespace PetFamily.Domain.PetsManagment.Aggregate;

public class Volunteer : SoftDeletableEntity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    private Volunteer(VolunteerId id) : base(id)
    {

    }

    public Volunteer(
        VolunteerId volunteerId,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Description description,
        Experience experience,
        SocialNetworkDetails socialNetworkDetails,
        RequisiteDetails requisiteDetails) : base(volunteerId)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
        Experience = experience;
        SocialNetworkDetails = socialNetworkDetails;
        RequisiteDetails = requisiteDetails;
    }

    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Experience Experience { get; private set; } = default!;
    public SocialNetworkDetails SocialNetworkDetails { get; private set; } = default!;
    public RequisiteDetails RequisiteDetails { get; private set; } = default!;
    public IReadOnlyList<Pet> Pets => _pets;
    public int CountPetsOnTreatment => _pets.Count(p => p.AssistanceStatus == AssistanceStatus.OnTreatment);
    public int CountPetsAtTheShelter => _pets.Count(p => p.AssistanceStatus == AssistanceStatus.AtTheShelter);
    public int CountPetsAtHome => _pets.Count(p => p.AssistanceStatus == AssistanceStatus.AtHome);

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Description description,
        Experience experience)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
        Experience = experience;
    }

    public void UpdateSocialNetwork(
        SocialNetworkDetails socialNetworkDetails)
    {
        SocialNetworkDetails = socialNetworkDetails;
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var position = Position.Create(_pets.Count + 1);
        if (position.IsFailure)
            return position.Error;

        pet.SetPosition(position.Value);
        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public void UpdatePetInfo(Pet pet)
    { 
        var index = _pets.FindIndex(p => p.Id == pet.Id);

        if (index != -1)
            _pets[index] = pet;
    }

    public UnitResult<Error> ShiftPetPosition(Pet pet, Position newPosition)
    {
        var oldPosition = pet.Position;

        if (oldPosition == newPosition)
            return Result.Success<Error>();

        var changePosition = ChangeNewPositionIfOutOfReach(newPosition);
        if (changePosition.IsFailure)
            return changePosition.Error;

        newPosition = changePosition.Value;


        if (newPosition.Value < oldPosition.Value)
        {
            var petsToMove = _pets.Where(p => p.Position.Value >= newPosition.Value
                                           && p.Position.Value < oldPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        else if (newPosition.Value > oldPosition.Value)
        {
            var petsToMove = _pets.Where(p => p.Position.Value <= newPosition.Value
                                           && p.Position.Value > oldPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        pet.SetPosition(newPosition);

        return Result.Success<Error>();
    }

    private Result<Position, Error> ChangeNewPositionIfOutOfReach(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }

    public override void Delete()
    {
        base.Delete();

        foreach (var pet in _pets)
            pet.Delete();
    }

    public override void Restore()
    {
        base.Restore();

        foreach (var pet in _pets)
            pet.Restore();
    }
}


