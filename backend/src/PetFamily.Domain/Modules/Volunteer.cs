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

        public Guid Id { get; private set; }
        public string FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int YearsOfExperience { get; private set; } = 0;
        public int CountOfShelterAnimals { get; private set; } = 0;
        public int CountOfHomelessAnimals { get; private set; } = 0;
        public int CountOfIllAnimals { get; private set; } = 0;
        public string PhoneNumber { get; private set; } = string.Empty;
        public List<SocialNetwork> SocialNetworks => _socialNetwork;
        public IReadOnlyList<Requisite> Requisite => _requisite;
        public List<Pet> Pets { get; private set; } = new();

        public void AddRequisite(Requisite requisite)
        {
            _requisite.Add(requisite);
        }
        public void AddSocialNetwork(SocialNetwork socialNetwork)
        {
            _socialNetwork.Add(socialNetwork);
        }
    }
}

