using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record GetVolunteersWithPaginationRequest(
    string? LastName, 
    int Page, 
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToCommand() =>
        new (LastName, Page, PageSize);
}