using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Specieses.Domain;

public record BreedName
{
    private BreedName(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<BreedName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("BreedName");

        return new BreedName(value);
    }
}