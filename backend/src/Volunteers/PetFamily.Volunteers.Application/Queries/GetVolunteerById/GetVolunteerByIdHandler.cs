using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Volunteers.Application;

namespace PetFamily.Volunteers.Application.Queries.GetVolunteerById;
public class GetVolunteerByIdHandler : IQueriesHandler<Result<VolunteerDto, Error>, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteerByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, Error>> Handle(GetVolunteerByIdQuery query, CancellationToken token = default)
    {
        var volunteerId = VolunteerId.Create(query.VolunteerId);

        var volunteerQuery = await _readDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == query.VolunteerId, token);

        if (volunteerQuery is null)
            return Errors.General.NotFound(volunteerId);

        return volunteerQuery;
    }
}


