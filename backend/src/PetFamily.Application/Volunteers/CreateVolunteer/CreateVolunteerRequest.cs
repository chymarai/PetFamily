using PetFamily.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;


namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(FullNameDto FullName, string Email, string PhoneNumber, string Description);
