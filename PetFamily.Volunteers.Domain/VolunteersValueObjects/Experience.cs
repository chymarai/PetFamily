using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.VolunteersValueObjects;
public record Experience
{
    private Experience(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Experience, Error> Create(string value)
    {
        return new Experience(value);
    }
}
