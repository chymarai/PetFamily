using PetFamily.Core.Abstraction;

namespace PetFamily.Specieses.Application.Commands.DeleteSpecies;
public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;
