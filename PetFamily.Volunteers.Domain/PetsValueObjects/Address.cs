using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;

namespace PetFamily.Volunteers.Domain.PetsValueObjects;

public record Address
{
    private Address(string country, string region, string city, string street)
    {
        Country = country;
        Region = region;
        City = city;
        Street = street;
    }
    public string Country { get; } = default!;
    public string Region { get; } = default!;
    public string City { get; } = default!;
    public string Street { get; } = default!;

    public static Result<Address, Error> Create(string country, string region, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Country");

        if (string.IsNullOrWhiteSpace(region) || region.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Region");

        if (string.IsNullOrWhiteSpace(city) || city.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("City");

        if (string.IsNullOrWhiteSpace(street) || street.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Street");

        return new Address(country, region, city, street);
    }
}
