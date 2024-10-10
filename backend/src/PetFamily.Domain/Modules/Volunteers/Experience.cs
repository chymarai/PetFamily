using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Volunteers;
public record Experience
{
    private Experience(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Experience, Shared.Error> Create(string value)
    {
        return new Experience(value);
    }
}
