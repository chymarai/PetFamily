using PetFamily.Core.Abstraction;

namespace PetFamily.Specieses.Application.Commands.CreateSpecies;
public record CreateSpeciesCommand(string SpeciesName) : ICommand;