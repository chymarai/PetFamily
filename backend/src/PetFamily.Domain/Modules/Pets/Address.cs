using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets
{
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

        public static Result<Address> Create(string country, string region, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(country))
                return "Country can not be empty";

            if (string.IsNullOrWhiteSpace(region))
                return "Region can not be empty";

            if (string.IsNullOrWhiteSpace(city))
                return "City can not be empty";

            if (string.IsNullOrWhiteSpace(street))
                return "Street can not be empty";

            return new Address(country, region, city, street);
        }
    }
}
