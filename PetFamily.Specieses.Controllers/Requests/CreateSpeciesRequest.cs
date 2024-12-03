using PetFamily.Specieses.Application.Commands.CreateSpecies;

namespace PetFamily.Specieses.Contracts.Requests;

public record CreateSpeciesRequest(string SpeciesName)
{
    public CreateSpeciesCommand ToCommand() =>
        new(SpeciesName);
}
