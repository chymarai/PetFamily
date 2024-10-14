using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using PetFamily.Domain.Modules.Pets;
using PetFamily.Domain.Modules.Volunteers;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted = false;

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
        Experience experience,
        RequisiteDetails requisiteDetails)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
        Experience = experience;
        RequisiteDetails = requisiteDetails;
    }

    public void UpdateSocialNetwork(
        SocialNetworkDetails socialNetworkDetails)
    {
        SocialNetworkDetails = socialNetworkDetails;
    }

    public void AddPet(Pet pet) => _pets.Add(pet);

    public void Delete() => _isDeleted = true;

    public void Restore() => _isDeleted = false;

    public static Result<Volunteer, Error> Create(
        VolunteerId volunteerId,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Description description,
        Experience experience,
        SocialNetworkDetails socialNetworkDetails,
        RequisiteDetails requisiteDetails)
    {
        return new Volunteer(
            volunteerId,
            fullName,
            email,
            phoneNumber,
            description,
            experience,
            socialNetworkDetails,
            requisiteDetails);
    }
}


