using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using PetFamily.Application.DTOs;
using PetFamily.Application.Models;
using PetFamily.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Abstraction;

public interface IQueriesHandler<TResponce, TQueries> where TQueries : IQueries
{
    Task<TResponce> Handle(TQueries query, CancellationToken token = default);
}