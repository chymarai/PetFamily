﻿using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record BreedId
{
    public BreedId()
    {

    }
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId NewBreedId() => new(Guid.NewGuid());
    public static BreedId Empty() => new(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);

    public static implicit operator BreedId(Guid id) => new(id);

    public static implicit operator Guid(BreedId breedId)
    {
        ArgumentNullException.ThrowIfNull(breedId);
        return breedId.Value;
    }
}