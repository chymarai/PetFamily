using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets
{
    public class Species : Entity<SpeciesId> //вид
    {
        public readonly List<Breed> _breed = new();
        private Species(SpeciesId id) : base(id)
        {
            
        }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breed;


    }
}
