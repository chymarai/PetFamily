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

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Species Species { get; private set; } = default!;






    }
}
