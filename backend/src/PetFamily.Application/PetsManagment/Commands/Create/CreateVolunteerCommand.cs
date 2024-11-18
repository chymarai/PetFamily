using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;


namespace PetFamily.Application.Volunteers.WriteHandler.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    SocialNetworkDetailsDto SocialNetworkDetails,
    RequisiteDetailsDto RequisiteDetails) : ICommand;
