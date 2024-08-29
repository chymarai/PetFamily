using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public class PetPhoto
    {
        public Guid Id { get; set; }
        public string Storage { get; private set; } = default!;
        public bool IsMain { get; private set; }
    }
}
