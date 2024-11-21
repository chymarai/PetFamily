using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Queries.GetVolunteerById;
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

        var volunteerQuery = await _readDbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == query.VolunteerId);

        if (volunteerQuery is null)
            return Errors.General.NotFound(volunteerId);

        return volunteerQuery;
    }
}
