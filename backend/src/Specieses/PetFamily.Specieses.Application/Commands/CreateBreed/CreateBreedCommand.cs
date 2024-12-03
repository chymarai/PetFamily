using PetFamily.Core.Abstraction;

namespace PetFamily.Specieses.Application.Commands.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string BreedName) : ICommand;
}
