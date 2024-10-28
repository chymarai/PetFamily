﻿using PetFamily.Application.DTOs;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain.Modules.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;


namespace PetFamily.API.Controllers.Volunteers.Contracts;

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

