using PetFamily.Application.DTOs;
using PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdateSocialNetworkRequest(
    SocialNetworkDetailsDto SocialNetworkDetails)
{
    public UpdateSocialNetworkCommand ToCommand(Guid VolunteerId) =>
        new(VolunteerId, SocialNetworkDetails);
}
