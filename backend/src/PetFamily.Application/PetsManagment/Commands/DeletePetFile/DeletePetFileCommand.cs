using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Commands.UpdatePetFiles;
public record DeletePetFileCommand(Guid VolunteerId, Guid PetId, string FilePath) : ICommand;
