namespace PetFamily.Core.DTOs;
public class VolunteerDto
{
    public Guid Id { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string MiddleName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Experience { get; init; } = string.Empty;
    public string SocialNetwork { get; init; } = string.Empty;
    public string Requisite { get; init; } = string.Empty;
}