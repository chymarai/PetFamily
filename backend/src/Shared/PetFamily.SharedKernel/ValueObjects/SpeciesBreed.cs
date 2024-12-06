using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Ids;

namespace PetFamily.SharedKernel.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public SpeciesId SpeciesId { get; }

    public Guid BreedId { get; }

    public static Result<SpeciesBreed, Error> Create(SpeciesId speciesId, Guid breedId)
    {
        return new SpeciesBreed(speciesId, breedId);
    }
}
