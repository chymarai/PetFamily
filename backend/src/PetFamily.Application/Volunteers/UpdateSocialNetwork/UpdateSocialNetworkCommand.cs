using PetFamily.Application.DTOs;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetwork;

public record UpdateSocialNetworkCommand(
    Guid VolunteerId,
    SocialNetworkDetailsDto SocialNetworkDetails);
