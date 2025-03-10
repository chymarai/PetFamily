﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.SharedKernel.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork() { }
    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }
    public string Name { get; } = default!;
    public string Url { get; } = default!;

    public static Result<SocialNetwork, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(url) || url.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Url");

        return new SocialNetwork(name, url);
    }
}
