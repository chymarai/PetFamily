using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public class Volunteer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int YearsOfExperience { get; set; } = 0;
        public int CountOfShelterAnimals { get; private set; } = 0;
        public int CountOfHomelessAnimals { get; private set; } = 0;
        public int CountOfIllAnimals { get; private set; } = 0;
        public string PhoneNumber { get; private set; } = string.Empty;
        public List<SocialNetwork> SocialNetworks { get; private set; } = new();
        public List<Requisite> Requisite { get; private set; } = new();
        public List<Pet> Pets { get; private set; } = new();



    }
}
