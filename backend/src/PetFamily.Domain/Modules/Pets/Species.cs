using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public class Species : Entity<SpeciesId>
{
    public Species(SpeciesId id) : base(id)
    {
        
    }

    public IReadOnlyList<Breed> Breeds { get; set; } = [];
}
