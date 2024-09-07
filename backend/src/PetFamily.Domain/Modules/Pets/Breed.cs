using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets
{
    public class Breed : Entity<BreedId> //порода
    {
        private Breed(BreedId id) : base(id)
        {
            
        }

        private Breed(BreedId breedId, string title, string description) : base(breedId)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        public static Result<Breed> Create(BreedId breedId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "Name can not be empty";


            if (string.IsNullOrWhiteSpace(description))
                return "Description can not be empty";

            return new Breed(breedId, title, description);
        }
    }
}
