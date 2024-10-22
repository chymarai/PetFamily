using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets;

public record Weight
{
    private const int MIN_VALUE = 0;
    private const int MAX_VALUE = 500;

    private Weight(int value)
    {
        Value = value;
    }
    public int Value { get; } = default!;

    public static Result<Weight, Error> Create(int value)
    {
        if (value > MIN_VALUE || value <= MAX_VALUE)
            return Errors.General.ValueIsInvalid("Weight");

        return new Weight(value);
    }
}