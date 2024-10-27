using PetFamily.Application.Specieses.Create;

namespace PetFamily.API.Controllers.Species.Request;

public record CreateSpeciesRequest(string SpeciesName)
{
    public CreateSpeciesCommand ToCommand() =>
        new (SpeciesName);
}
