using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public class Volunteer
    {
        private readonly List<Requisite> _requisite = [];

        private readonly List<SocialNetwork> _socialNetwork = [];

        private readonly List<Pet> _pet = [];

        public Guid Id { get; private set; }
        public string FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int YearsOfExperience { get; private set; } = 0;
        public int CountOfShelterAnimals { get; private set; } = 0;
        public int CountOfHomelessAnimals { get; private set; } = 0;
        public int CountOfIllAnimals { get; private set; } = 0;
        public string PhoneNumber { get; private set; } = string.Empty;
        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetwork;
        public IReadOnlyList<Requisite> Requisite => _requisite;
        public IReadOnlyList<Pet> Pet => _pet;

        public void AddRequisite(Requisite requisite)
        {
            _requisite.Add(requisite);
        }
        public void AddSocialNetwork(SocialNetwork socialNetwork)
        {
            _socialNetwork.Add(socialNetwork);
        }
        public void AddPet(Pet pet)
        {
            _pet.Add(pet);
        }
    }
}

