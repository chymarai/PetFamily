using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Abstraction;
public interface IQueriesHandler<TResponce, TQueries> where TQueries : IQueries
{
    Task<PagedList<TResponce>> Handle(TQueries query, CancellationToken token = default);
}
