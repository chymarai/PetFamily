namespace PetFamily.Application.DTOs;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public Guid SpeciesId {  get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime Birthdate { get; init; } = default!;
    public string Country { get; init; } = string.Empty ;
    public string Region { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public int Weight { get; init; } = default!;
    public int Height { get; init; } = default!;
    public bool IsCastrated { get; init; }
    public bool IsVaccination { get; init; }
    public string AssistanceStatus {  get; init; }
    public string Color { get; init; } = string.Empty;
    public string HealthInformation { get; init; } = string.Empty;
    public DateTime DateOfCreation { get; init; } = default!;
    public PetFileDto[] Files { get; private set; } = null!;
}
