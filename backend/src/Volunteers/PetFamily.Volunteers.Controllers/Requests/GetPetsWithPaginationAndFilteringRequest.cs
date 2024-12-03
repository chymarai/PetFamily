using PetFamily.Volunteers.Application.Queries.GetPetsWithPaginationAndFiltering;

namespace PetFamily.Volunteers.Presentation.Requests;

public record GetPetsWithPaginationAndFilteringRequest(
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
    int PageSize)
{
    public GetPetsWithPaginationAndFilteringQuery ToCommand() =>
        new(VolunteerId,
            BreedId,
            SpeciesId,
            BreedName,
            Name,
            Color,
            Country,
            Region,
            City,
            WeightFrom,
            WeightTo,
            HeightFrom,
            HeightTo,
            IsCastrated,
            IsVaccination,
            AssistanceStatus,
            AgeFrom,
            AgeTo,
            DateOfCreation,
            OrderBy,
            OrderByDescending,
            Page,
            PageSize);
};