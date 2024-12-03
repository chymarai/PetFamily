using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Application;
using PetFamily.Specieses.Domain;
using PetFamily.Specieses.Infrastructure.DbContexts;

namespace PetFamily.Specieses.Infrastructure;

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