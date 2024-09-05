using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Volunteers
{
    public record Requisite
    {
        private Requisite(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; } = default!;
        public string Description { get; } = default!;

        public static Result<Requisite> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name can not be empty";


            if (string.IsNullOrWhiteSpace(description))
                return "Description can not be empty";

            var requisite = new Requisite(name, description);

            return requisite;
        }
    }
}
