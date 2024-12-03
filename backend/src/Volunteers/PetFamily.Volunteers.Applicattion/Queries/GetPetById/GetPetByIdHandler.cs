using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;

namespace PetFamily.Volunteers.Application.Queries.GetPetById;
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
