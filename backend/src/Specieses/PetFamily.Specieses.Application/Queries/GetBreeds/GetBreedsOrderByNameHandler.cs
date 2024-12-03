using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.Core.Models;

namespace PetFamily.Specieses.Application.Queries.GetBreeds;
public class GetBreedsOrderByNameHandler : IQueriesHandler<OnePageList<BreedDto>, GetBreedsOrderByNameQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetBreedsOrderByNameHandler(
        IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<OnePageList<BreedDto>> Handle(GetBreedsOrderByNameQuery query, CancellationToken token = default)
    {
        var breeds = await _readDbContext.Breeds
            .Where(b => b.SpeciesId == query.SpeciesId)
            .ToListAsync(token);

        var totalCount = breeds.Count;

        return new OnePageList<BreedDto>
        {
            Items = breeds,
            TotalCount = totalCount
        };
    }
}
