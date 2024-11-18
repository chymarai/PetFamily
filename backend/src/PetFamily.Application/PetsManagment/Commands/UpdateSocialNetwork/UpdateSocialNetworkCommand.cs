using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.Volunteers.WriteHandler.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkDetailsDto SocialNetworkDetails) : ICommand;
