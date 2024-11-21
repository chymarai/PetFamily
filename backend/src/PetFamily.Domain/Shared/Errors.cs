using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared;
public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";

            return Error.NotFound("record.not.found", $"record not found for{forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";

            return Error.Validation("length.is.invalid", $"invalid{label}length");
        }
    }

    public static class Volunteer
    {
        public static Error AlreadyExist(string? name = null)
        {
            var label = name ?? "value";

            return Error.Validation("record.already.exist", $"{label} already exist");
        }
    }

    public static class Species
    {
        public static Error Exist(Guid? id = null)
        {
            var Id = id == null ? "" : $" {id}";

            return Error.Conflict("animal.of.this.species.exist", $"animal.of.this.species{Id}.exist");
        }
    }

    public static class Breed
    {
        public static Error Exist(Guid? id = null)
        {
            var Id = id == null ? "" : $" {id}";

            return Error.Conflict("animal.of.this.breed.exist", $"animal.of.this.breed{Id}.exist");
        }
    }
}


