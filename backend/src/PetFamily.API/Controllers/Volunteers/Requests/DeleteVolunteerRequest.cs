﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.API.Controllers.Volunteers.Requests;
public record DeleteVolunteerRequest(Guid VolunteerId);