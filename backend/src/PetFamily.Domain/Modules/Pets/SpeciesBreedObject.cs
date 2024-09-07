using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets
{
    public class SpeciesBreedObject
    {
        private SpeciesBreedObject(Guid breedId, SpeciesId speciesId)
        {
            BreedId = breedId;

            SpeciesId = speciesId;
        }

        public Guid BreedId { get; set; } = default!;
        public SpeciesId SpeciesId { get; set; } = default!;

        public static Result<SpeciesBreedObject> Create(Guid breedId, SpeciesId speciesId)
        {
            return new SpeciesBreedObject(breedId, speciesId);
        }
    }
}
