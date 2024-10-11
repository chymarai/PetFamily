using PetFamily.Application.DTOs;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;

public record UpdateSocialNetworkRequest(
    Guid VolunteerId,
    UpdateSocialNetworkDto Dto);

public record UpdateSocialNetworkDto(SocialNetworkDetailsDto SocialNetworkDetails);
