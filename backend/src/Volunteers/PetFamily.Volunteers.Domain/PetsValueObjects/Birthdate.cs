﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.PetsValueObjects;

public record BirthDate
{
    private const int MIN_YEAR_BIRTHDAY = 1960;

    public BirthDate() { }

    public BirthDate(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<BirthDate, Error> Create(DateTime value)
    {
        if (value.Year < MIN_YEAR_BIRTHDAY || value > DateTime.Now)
            return Errors.General.ValueIsInvalid("Birthday");

        return new BirthDate(value);
    }
}
