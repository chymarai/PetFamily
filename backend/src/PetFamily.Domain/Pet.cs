namespace PetFamily.Domain
{
    public class Pet
    {
        private bool vaccination = default!;

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
        public bool Castrated { get; set; } = default!;
        public DateOnly DateOnly { get; set; } = default!;
        public bool Vaccination { get => vaccination; set => vaccination = value; }
        public string AssistanceStatus { get; set; } = default!;
        public Requisites Requisites { get; set; } = default!;
        public DateTime DateOfCreation { get; set; } = default!;
    }
}
