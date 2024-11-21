using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Specieses;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesesRepository : ISpeciesesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Specieses.AddAsync(species, cancellationToken);

        return species.Id;
    }
    public Guid Save(Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Attach(species);

        return species.Id;
    }

    public Guid Delete(Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Specieses.Remove(species);

        return species.Id;
    }

    public async Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Specieses
            .Include(m => m.Breeds)
            .FirstOrDefaultAsync(m => m.Id == speciesId);

        if (volunteer is null)
            return Errors.General.NotFound(speciesId);

        return volunteer;
    }
}