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

        private Species(SpeciesId speciesId, string title, string description) : base(speciesId)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breed;

        public static Result<Species> Create(SpeciesId speciesId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "Name can not be empty";


            if (string.IsNullOrWhiteSpace(description))
                return "Description can not be empty";

            return new Species(speciesId, title, description);
        }
    }
}
