using PetFamily.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Queries.GetVolunteersWithPagination;
public record GetVolunteersWithPaginationQuery(
    string? LastName,
    int Page,
    int PageSize) : IQueries;
