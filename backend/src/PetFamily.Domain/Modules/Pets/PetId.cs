using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets
{
    public record PetId
    {
        public PetId()
        {
            
        }
        private PetId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetId NewVolunteerId() => new(Guid.NewGuid());
        public static PetId Empty() => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);
    }
}
