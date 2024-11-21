using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.BreedsManagment.DeleteSpecies;
public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;
