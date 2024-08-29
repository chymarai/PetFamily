using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules
{
    public class Pet
    {
        private readonly List<Requisite> _requisite = [];

        private readonly List<PetPhoto> _petPhoto = [];

        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Type { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Breed { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HealthInformation { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public int Weight { get; private set; } = default!;
        public int Height { get; private set; } = default!;
        public int PhoneNumber { get; private set; } = default!;
        public bool IsCastrated { get; private set; } = default!;
        public bool IsVaccination { get; private set; }
        public string AssistanceStatus { get; private set; } = default!;
        public DateOnly BirthDate { get; private set; } = default!;
        public DateTime DateOfCreation { get; private set; } = default!;
        public IReadOnlyList<Requisite> Requisite => _requisite;
        public IReadOnlyList<PetPhoto> PetPhoto => _petPhoto;
        
        public void AddRequisite(Requisite requisite)
        {
            _requisite.Add(requisite);
        }
        public void AddPetPhoto(PetPhoto petPhoto)
        {
            _petPhoto.Add(petPhoto);
        }
    }
}
