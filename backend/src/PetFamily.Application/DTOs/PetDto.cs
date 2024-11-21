namespace PetFamily.Application.DTOs;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public Guid SpeciesId {  get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public string HealthInformation { get; init; } = string.Empty;
    public string Files { get; private set; } = string.Empty;
}
