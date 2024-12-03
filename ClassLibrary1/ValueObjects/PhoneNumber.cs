using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;
public record PhoneNumber
{
    private const string PhoneRegex = "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$";

    private PhoneNumber(string value)
    {
        Value = value;
    }
    public string Value { get; } = default!;

    public static Result<PhoneNumber, Error> Create(string input)
    {
        //var number = input.Trim();

        //if (Regex.IsMatch(number, PhoneRegex) == false)
        //    return Errors.General.ValueIsInvalid("Phone number");

        return new PhoneNumber(input);
    }
}
