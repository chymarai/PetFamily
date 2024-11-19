using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.SpeciesManagment;

public class Species : Entity<SpeciesId>
{
    private List<Breed> _breeds = [];

    private Species(SpeciesId id) : base(id)
    {

    }

    public Species(SpeciesId id, SpeciesName speciesName) : base(id)
    {
        SpeciesName = speciesName;
    }

    public SpeciesName SpeciesName { get; } = default!;

    public IReadOnlyList<Breed> Breeds => _breeds;

    public void AddBreed(Breed breed) => _breeds.Add(breed);
}

