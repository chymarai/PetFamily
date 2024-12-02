using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetFamily.Application.PetsManagment.Queries.GetPetsWithId;
public class GetPetByIdHandler : IQueriesHandler<Result<PetDto, Error>, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<PetDto, Error>> Handle(GetPetByIdQuery query, CancellationToken token = default)
    {
        var petId = PetId.Create(query.PetId);

        var petQuery = await _readDbContext.Pets.FirstOrDefaultAsync(v => v.Id == query.PetId, token);

        if (petQuery is null)
            return Errors.General.NotFound(petId);

        return petQuery;
    }
}
