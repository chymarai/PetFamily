using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.PetsValueObjects;

public record HealthInformation
{
    private HealthInformation(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<HealthInformation, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("HealthInformation");

        return new HealthInformation(value);
    }
}