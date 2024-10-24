using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.PetsManagment.ValueObjects.Pets
{
    public record Birthday
    {
        private const int MIN_YEAR_BIRTHDAY = 1960;

        public Birthday() { }

        public Birthday(DateOnly value)
        {
            Value = value;
        }

        public DateOnly Value { get; } 

        public static Result<Birthday, Error> Create(DateOnly value)
        {
            if (value.Year < MIN_YEAR_BIRTHDAY || value > DateOnly.FromDateTime(DateTime.Now))
                return Errors.General.ValueIsInvalid("Birthday");

            return new Birthday(value);
        }
    }
}
