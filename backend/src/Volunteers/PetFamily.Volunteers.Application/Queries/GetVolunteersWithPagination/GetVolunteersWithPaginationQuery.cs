using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.GetVolunteersWithPagination;
public record GetVolunteersWithPaginationQuery(
    string? LastName,
    int Page,
    int PageSize) : IQueries;
