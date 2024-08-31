using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public record Requisite
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
