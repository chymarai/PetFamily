using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.VolunteersValueObjects;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Volunteers.Infrastructure;

public class WriteVolunteersRepository : IWriteVolunteersRepository
{
    private readonly WriteDbContext _dbContext;

    public WriteVolunteersRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }
    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Attach(volunteer);

        return volunteer.Id;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(m => m.Pets)
            .FirstOrDefaultAsync(m => m.Id == volunteerId);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
           .Include(m => m.Pets)    //для таблиц, для VO не надо
           .FirstOrDefaultAsync(m => m.Email == email);

        if (volunteer is null)
            return Errors.General.NotFound();

        return volunteer;
    }
}
