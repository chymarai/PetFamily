using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Application;
using PetFamily.Specieses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Specieses.Infrastructure;
public class SpeciesContract(IReadDbContext readDbContext) : ISpeciesContract
{
    public async Task<Result<BreedDto, Error>> BreedExists(Guid breedId, CancellationToken token)
    {
        var result = await readDbContext.Breeds.FirstOrDefaultAsync(x => x.Id == breedId, token);

        if (result != null)
            return result;

        return Errors.General.NotFound(breedId);
    }

    public async Task<Result<SpeciesDto, Error>> SpeciesExist(Guid speciesId, CancellationToken token)
    {
        var result = await readDbContext.Specieses.FirstOrDefaultAsync(b => b.Id == speciesId, token);

        if (result != null)
            return result;

        return Errors.General.NotFound(speciesId);
    }
}
