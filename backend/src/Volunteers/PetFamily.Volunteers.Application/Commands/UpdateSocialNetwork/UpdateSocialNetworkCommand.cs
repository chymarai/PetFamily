using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;

namespace PetFamily.Volunteers.Application.Commands.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkDetailsDto SocialNetworkDetails) : ICommand;
