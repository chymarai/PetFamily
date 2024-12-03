using PetFamily.Core.DTOs;
using PetFamily.Volunteers.Application.Commands.UpdateSocialNetwork;

namespace PetFamily.Volunteers.Presentation.Requests;

public record UpdateSocialNetworkRequest(
    SocialNetworkDetailsDto SocialNetworkDetails)
{
    public UpdateSocialNetworkCommand ToCommand(Guid VolunteerId) =>
        new(VolunteerId, SocialNetworkDetails);
}
