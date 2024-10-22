using PetFamily.Application.DTOs;


namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    SocialNetworkDetailsDto SocialNetworkDetails,
    RequisiteDetailsDto RequisiteDetails);
