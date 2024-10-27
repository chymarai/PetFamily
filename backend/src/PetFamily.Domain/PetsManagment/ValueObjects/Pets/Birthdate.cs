using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record Birthdate
{
    private const int MIN_YEAR_BIRTHDAY = 1960;

    public Birthdate() { }

    public Birthdate(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; } 

    public static Result<Birthdate, Error> Create(DateTime value)
    {
        if (value.Year < MIN_YEAR_BIRTHDAY || value > DateTime.Now)
            return Errors.General.ValueIsInvalid("Birthday");

        return new Birthdate(value);
    }
}
