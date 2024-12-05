using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;


namespace PetFamily.Volunteers.Application.Commands.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    SocialNetworkDetailsDto SocialNetworkDetails,
    RequisiteDetailsDto RequisiteDetails) : ICommand;
