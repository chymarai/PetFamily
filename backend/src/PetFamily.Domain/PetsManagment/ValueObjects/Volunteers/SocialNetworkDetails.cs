﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;

public record SocialNetworkDetails
{
    private SocialNetworkDetails()
    {

    }

    private SocialNetworkDetails(IEnumerable<SocialNetwork> value)
    {
        Value = value.ToList();
    }

    public IReadOnlyList<SocialNetwork> Value { get; }

    public static SocialNetworkDetails Create(IEnumerable<SocialNetwork> value)
    {
        return new SocialNetworkDetails(value);
    }
}
