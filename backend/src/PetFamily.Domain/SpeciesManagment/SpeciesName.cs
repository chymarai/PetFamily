using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.SpeciesManagment;

public record SpeciesName
{
    private SpeciesName(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<SpeciesName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("SpeciesName");

        return new SpeciesName(value);
    }
}
