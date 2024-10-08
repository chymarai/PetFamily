﻿using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public class Species : Entity<SpeciesId>
{
    private Species(SpeciesId id) : base(id)
    {
        
    }

    private Species(SpeciesId id, IReadOnlyList<Breed> breeds) : base(id)
    {
        Breeds = breeds;
    }
    public IReadOnlyList<Breed> Breeds { get; } = [];

    public static Species Create(SpeciesId id, IReadOnlyList<Breed> breeds)
    {
        return new Species(id, breeds);
    }
}
