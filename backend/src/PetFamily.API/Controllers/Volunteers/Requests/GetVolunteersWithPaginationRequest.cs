using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(int page, int pageSize)
{
    public GetVolunteersWithPaginationQuery ToCommand() =>
        new (page, pageSize);
}

