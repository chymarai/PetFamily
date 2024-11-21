using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Application.Volunteers.WriteHandler.Create;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
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


