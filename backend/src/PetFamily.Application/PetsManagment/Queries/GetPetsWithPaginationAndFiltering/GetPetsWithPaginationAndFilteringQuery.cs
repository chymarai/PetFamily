using PetFamily.Application.Abstraction;
using PetFamily.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetsManagment.Queries.GetPetsWithPaginationAndFiltering;
public record GetPetsWithPaginationAndFilteringQuery(
    Guid? VolunteerId,
    Guid? BreedId, 
    Guid? SpeciesId, 
    string? BreedName, 
    string? Name,
    string? Color,
    string? Country,
    string? Region,
    string? City,
    int? WeightFrom,
    int? WeightTo,
    int? HeightFrom,
    int? HeightTo,
    bool? IsCastrated,
    bool? IsVaccination,
    string? AssistanceStatus,
    int? AgeFrom,
    int? AgeTo,
    DateTime? DateOfCreation,
    string? OrderBy,
    string? OrderByDescending,
    int Page,
    int PageSize) : IQueries;