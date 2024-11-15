using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
public record GetVolunteersWithPaginationQuery(int Page, int PageSize);
