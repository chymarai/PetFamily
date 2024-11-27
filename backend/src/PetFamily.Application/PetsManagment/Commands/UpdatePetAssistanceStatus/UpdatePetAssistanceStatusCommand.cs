using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.UpdatePetAssistanceStatus;
public record UpdatePetAssistanceStatusCommand(
    Guid VolunteerId, 
    Guid PetId, 
    string AssistanceStatus) : ICommand;
