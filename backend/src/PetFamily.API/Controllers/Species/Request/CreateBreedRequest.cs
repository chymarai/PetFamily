using PetFamily.Application.Specieses.CreateBreed;

namespace PetFamily.API.Controllers.Species.Request;

public record CreateBreedRequest(string BreedName)
{
    public CreateBreedCommand ToCommand(Guid id) =>  
        new (id, BreedName);
}
