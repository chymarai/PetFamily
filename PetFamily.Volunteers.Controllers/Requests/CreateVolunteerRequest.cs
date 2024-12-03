using PetFamily.Core.DTOs;
using PetFamily.Volunteers.Application.Commands.Create;

namespace PetFamily.Volunteers.Presentation.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string PhoneNumber,
    string Description,
    string Experience,
    SocialNetworkDetailsDto SocialNetworkDetails,
    RequisiteDetailsDto RequisiteDetails)
{
    public CreateVolunteerCommand ToCommand() =>
        new(FullName, Email, PhoneNumber, Description, Experience, SocialNetworkDetails, RequisiteDetails);
}


