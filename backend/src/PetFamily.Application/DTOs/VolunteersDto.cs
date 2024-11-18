using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Shared;
using PetFamily.Domain.PetsManagment.ValueObjects.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs;
public class VolunteerDto
{
    public Guid Id { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string FirstName {  get; init; } = string.Empty;
    public string MiddleName {  get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Experience { get; init; } = string.Empty;
    public string SocialNetwork { get; init; } = string.Empty;
    public string Requisite { get; init; } = string.Empty;
}