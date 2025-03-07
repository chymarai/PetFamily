using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.Core.Models;

namespace PetFamily.Specieses.Application.Queries.GetSpecieses;
public class GetSpeciesesOrderByNameHandler : IQueriesHandler<OnePageList<SpeciesDto>>
{
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesesOrderByNameHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<OnePageList<SpeciesDto>> Handle(CancellationToken token = default)
    {

        var specieses = await _readDbContext.Specieses
            .OrderBy(s => s.SpeciesName)
            .ToListAsync(token);

        var totalCount = specieses.Count;

        return new OnePageList<SpeciesDto>
        {
            Items = specieses,
            TotalCount = totalCount
        };
    }
}


