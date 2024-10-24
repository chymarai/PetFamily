using PetFamily.Application.DTOs;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;

public record UpdateSocialNetworkRequest(
    SocialNetworkDetailsDto SocialNetworkDetails)
{
    public UpdateSocialNetworkCommand ToCommand(Guid VolunteerId) =>
        new(VolunteerId, SocialNetworkDetails);
}
