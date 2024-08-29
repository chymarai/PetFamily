using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string HealthInformation { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int Weight { get; set; } = default!;
        public int Height { get; set; } = default!;
        public int PhoneNumber { get; set; } = default!;
        public bool IsCastrated { get; set; } = default!;
        public bool IsVaccination { get; set; }
        public string AssistanceStatus { get; set; } = default!;
        public DateOnly BirthDate { get; set; } = default!;
        public DateTime DateOfCreation { get; set; } = default!;
        public List<Requisite> Requisite { get; set; } = new();
        public List<PetPhoto> PetPhoto { get;  } = default!;
    }
}
