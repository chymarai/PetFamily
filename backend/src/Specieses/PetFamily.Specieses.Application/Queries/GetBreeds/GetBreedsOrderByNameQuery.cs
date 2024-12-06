using PetFamily.Core.Abstraction;

namespace PetFamily.Specieses.Application.Queries.GetBreeds;
public record GetBreedsOrderByNameQuery(Guid SpeciesId) : IQueries;
