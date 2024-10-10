using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

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
