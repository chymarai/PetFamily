using PetFamily.Core.Abstraction;

namespace PetFamily.Specieses.Application.Commands.DeleteBreed;
public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
