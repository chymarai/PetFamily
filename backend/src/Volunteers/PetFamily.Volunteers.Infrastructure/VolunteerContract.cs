using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Infrastructure;
public class VolunteerContract(IReadDbContext context) : IVolunteerContract
{
    public async Task<Result<PetDto, Error>> PetUsedBreed(Guid breedId, CancellationToken token = default)
    {

        var result = await context.Pets.FirstOrDefaultAsync(p => p.BreedId == breedId, token);
        if (result == null)
            return result;

        return Errors.Breed.Exist(breedId);
    }

    public async Task<Result<PetDto, Error>> PetUsedSpecies(Guid speciesId, CancellationToken token = default)
    {
        var result = await context.Pets.FirstOrDefaultAsync(p => p.SpeciesId == speciesId, token);
        if (result == null)
            return result;

        return Errors.Species.Exist(speciesId);
    }
}
