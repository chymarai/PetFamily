using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;

public record Requisite
{
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public string Name { get; } = default!;
    public string Description { get; } = default!;

    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Description");


        return new Requisite(name, description);
    }
}
