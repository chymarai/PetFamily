using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record Color
{
    private Color(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Color");

        return new Color(value);
    }
}