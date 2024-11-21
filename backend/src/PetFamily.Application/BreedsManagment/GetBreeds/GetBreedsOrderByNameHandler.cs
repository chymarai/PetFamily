using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.BreedsManagment.GetBreeds;
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
