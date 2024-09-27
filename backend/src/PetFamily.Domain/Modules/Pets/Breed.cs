using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public class Breed :Entity<BreedId>
{
    public Breed(BreedId id) : base(id)
    {
        
    }

}
