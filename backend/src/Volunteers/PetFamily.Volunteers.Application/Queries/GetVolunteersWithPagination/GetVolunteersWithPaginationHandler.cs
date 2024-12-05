using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.Core.Models;
using PetFamily.Volunteers.Application;

namespace PetFamily.Volunteers.Application.Queries.GetVolunteersWithPagination;
public class GetVolunteersWithPaginationHandler : IQueriesHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken token = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        var totalCount = await volunteersQuery.CountAsync(token);

        var volunteers = await _readDbContext.Volunteers
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(token);

        return new PagedList<VolunteerDto>
        {
            Items = volunteers,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}


