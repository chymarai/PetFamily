using CSharpFunctionalExtensions;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

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