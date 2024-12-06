using PetFamily.Volunteers.Application.Queries.GetVolunteersWithPagination;

namespace PetFamily.Volunteers.Presentation.Requests;

public record GetVolunteersWithPaginationRequest(
    string? LastName,
    int Page,
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToCommand() =>
        new(LastName, Page, PageSize);
}