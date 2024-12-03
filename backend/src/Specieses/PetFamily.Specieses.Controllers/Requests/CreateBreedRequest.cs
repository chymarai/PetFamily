using PetFamily.Specieses.Application.Commands.CreateBreed;

namespace PetFamily.Specieses.Contracts.Requests;

public record CreateBreedRequest(string BreedName)
{
    public CreateBreedCommand ToCommand(Guid id) =>
        new(id, BreedName);
}
