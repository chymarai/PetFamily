using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Modules.Pets;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.Domain.Modules.Volunteers
{
    public class Volunteer : Entity<VolunteerId>
    {
        private Volunteer(VolunteerId id) : base(id)
        {

        }

        public Volunteer(VolunteerId volunteerId, string fullname, string description) : base(volunteerId)
        {
            FullName = fullname;
            Description = description;
        }

        private readonly List<Pet> _pet = [];

        public string FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int YearsOfExperience { get; private set; } = 0;
        public int CountOfShelterAnimals { get; private set; } = 0;
        public int CountOfHomelessAnimals { get; private set; } = 0;
        public int CountOfIllAnimals { get; private set; } = 0;
        public string PhoneNumber { get; private set; } = string.Empty;
        public SocialNetworkDetails SocialNetworkDetails { get; private set; }
        public RequisiteDetails RequisiteDetails { get; private set; }
        public IReadOnlyList<Pet> Pet => _pet;


        public void AddPet(Pet pet)
        {
            _pet.Add(pet);
        }

        public static Result<Volunteer> Create(VolunteerId volunteerId, string fullName, string description)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "Name can not be empty";


            if (string.IsNullOrWhiteSpace(description))
                return "Description can not be empty";

            return new Volunteer(volunteerId, fullName, description);
        }
    }
}

