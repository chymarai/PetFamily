using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Modules.Pets;
using PetFamily.Domain.Modules.Volunteers;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {

    }

    public Volunteer(VolunteerId volunteerId, FullName fullName, Email email, PhoneNumber phoneNumber, Description description) : base(volunteerId)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Description = description;
    }

    private readonly List<Pet> _pet = [];

    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public int YearsOfExperience { get; private set; } = 0;
    public int CountOfShelterAnimals { get; private set; } = 0;
    public int CountOfHomelessAnimals { get; private set; } = 0;
    public int CountOfIllAnimals { get; private set; } = 0;
    public SocialNetworkDetails SocialNetworkDetails { get; private set; } = default!;
    public RequisiteDetails RequisiteDetails { get; private set; } = default!;
    public IReadOnlyList<Pet> Pet => _pet;

    public void AddPet(Pet pet)
    {
        _pet.Add(pet);
    }

    public static Result<Volunteer, Error> Create(VolunteerId volunteerId, FullName fullName, Email email, PhoneNumber phoneNumber, Description description)
    {
        return new Volunteer(volunteerId, fullName, email, phoneNumber, description);
    }
}

