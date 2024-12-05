using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;

public record Description
{
    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; } = default!;

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        return new Description(value);
    }
}
