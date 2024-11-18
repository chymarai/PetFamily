using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.WriteHandler.DeleteVolunteer;
public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;