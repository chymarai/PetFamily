using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.PetsValueObjects;

public record Name
{
    private Name(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        return new Name(value);
    }
}