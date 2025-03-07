﻿using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;

namespace PetFamily.Specieses.Domain;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

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

    public void DeleteBreed(Breed breed) => _breeds.Remove(breed);
}

