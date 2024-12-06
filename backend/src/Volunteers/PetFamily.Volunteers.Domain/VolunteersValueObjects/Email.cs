using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.VolunteersValueObjects;

public record Email
{
    private static readonly string EmailRegex = @"^[\w-\.]{1,40}@([\w-]+\.)+[\w-]{2,4}$";

    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_LOW_TEXT_LENGTH/* || Regex.IsMatch(value, EmailRegex) == false*/)
            return Errors.General.ValueIsInvalid("Email");

        return new Email(value);
    }
}

