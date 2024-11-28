using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.SoftDeletePet;
public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
