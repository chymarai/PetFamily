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

namespace PetFamily.Application.BreedsManagment.GetSpecies;
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
