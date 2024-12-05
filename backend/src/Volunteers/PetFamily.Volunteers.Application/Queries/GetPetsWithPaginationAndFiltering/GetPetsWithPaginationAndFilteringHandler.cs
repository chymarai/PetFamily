using PetFamily.Core.Abstraction;
using PetFamily.Core.DTOs;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Volunteers.Application;

namespace PetFamily.Volunteers.Application.Queries.GetPetsWithPaginationAndFiltering;
public class GetPetsWithPaginationAndFilteringHandler : IQueriesHandler<PagedList<PetDto>, GetPetsWithPaginationAndFilteringQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetsWithPaginationAndFilteringHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<PagedList<PetDto>> Handle(GetPetsWithPaginationAndFilteringQuery query, CancellationToken token = default)
    {
        var petsQuery = _readDbContext.Pets;

        var totalCount = petsQuery.Count();

        var yearFrom = 0;
        var yearTo = 0;

        var petBirthdateFrom = DateTime.Now;
        var petBirthdateTo = DateTime.Now;

        if (query.AgeFrom > 0)
        {
            yearFrom = DateTime.Now.Year - query.AgeFrom.Value;
            petBirthdateFrom = new DateTime(yearFrom, DateTime.Now.Month, DateTime.Now.Day);
        }

        if (query.AgeTo > 0)
        {
            yearTo = DateTime.Now.Year - query.AgeTo.Value;
            petBirthdateTo = new DateTime(yearTo, DateTime.Now.Month, DateTime.Now.Day);
        }

        petsQuery = petsQuery
            .WhereIf(!string.IsNullOrWhiteSpace(query.SpeciesId.ToString()), b => b.SpeciesId == query.SpeciesId)
            .WhereIf(!string.IsNullOrWhiteSpace(query.BreedId.ToString()), p => p.BreedId == query.BreedId)
            .WhereIf(!string.IsNullOrWhiteSpace(query.Name), p => p.Name == query.Name)
            .WhereIf(!string.IsNullOrWhiteSpace(query.Color), p => p.Color == query.Color)
            .WhereIf(!string.IsNullOrWhiteSpace(query.Country), p => p.Country.Contains(query.Country))
            .WhereIf(!string.IsNullOrWhiteSpace(query.Region), p => p.Region.Contains(query.Region))
            .WhereIf(!string.IsNullOrWhiteSpace(query.City), p => p.City.Contains(query.City))
            .WhereIf(query.WeightFrom != null, p => p.Weight >= query.WeightFrom)
            .WhereIf(query.WeightTo != null, p => p.Weight <= query.WeightTo)
            .WhereIf(query.HeightFrom != null, p => p.Height >= query.HeightFrom)
            .WhereIf(query.HeightTo != null, p => p.Height <= query.HeightTo)
            .WhereIf(yearFrom != 0, p => p.Birthdate.Year <= petBirthdateFrom.Year)
            .WhereIf(yearTo != 0, p => p.Birthdate.Year >= petBirthdateTo.Year);

        if (query.IsCastrated == true)
            petsQuery = petsQuery.Where(p => p.IsCastrated == true);
        else if (query.IsCastrated == false)
            petsQuery = petsQuery.Where(p => p.IsCastrated == false);

        if (query.IsVaccination == true)
            petsQuery = petsQuery.Where(p => p.IsVaccination == true);
        else if (query.IsVaccination == false)
            petsQuery = petsQuery.Where(p => p.IsVaccination == false);

        if (query.AssistanceStatus != null)
        {
            if (query.AssistanceStatus.Contains("OnTreatment"))
                petsQuery = petsQuery.Where(p => p.AssistanceStatus == "OnTreatment");
            else if (query.AssistanceStatus.Contains("LookingHome"))
                petsQuery = petsQuery.Where(p => p.AssistanceStatus == "LookingHome");
            else if (query.AssistanceStatus.Contains("AtHome"))
                petsQuery = petsQuery.Where(p => p.AssistanceStatus == "AtHome");
        }

        if (query.OrderBy != null && query.OrderByDescending == null) //сортировка
        {
            switch (query.OrderBy)
            {
                case "Date":
                    petsQuery = petsQuery.OrderBy(x => x.DateOfCreation);
                    break;
                case "Name":
                    petsQuery = petsQuery.OrderBy(x => x.Name);
                    break;
                case "Age":
                    petsQuery = petsQuery.OrderBy(x => x.Birthdate);
                    break;
                case "Species":
                    petsQuery = petsQuery.OrderBy(x => x.SpeciesId);
                    break;
                case "Breed":
                    petsQuery = petsQuery.OrderBy(x => x.BreedId);
                    break;
                case "Color":
                    petsQuery = petsQuery.OrderBy(x => x.Color);
                    break;
                case "Volunteer":
                    petsQuery = petsQuery.OrderBy(x => x.VolunteerId);
                    break;
            }
        }
        else if (query.OrderByDescending != null && query.OrderBy == null)
        {
            switch (query.OrderByDescending)
            {
                case "Date":
                    petsQuery = petsQuery.OrderByDescending(x => x.DateOfCreation);
                    break;
                case "Name":
                    petsQuery = petsQuery.OrderByDescending(x => x.Name);
                    break;
                case "Age":
                    petsQuery = petsQuery.OrderByDescending(x => x.Birthdate);
                    break;
                case "Species":
                    petsQuery = petsQuery.OrderByDescending(x => x.SpeciesId);
                    break;
                case "Breed":
                    petsQuery = petsQuery.OrderByDescending(x => x.BreedId);
                    break;
                case "Color":
                    petsQuery = petsQuery.OrderByDescending(x => x.Color);
                    break;
                case "Volunteer":
                    petsQuery = petsQuery.OrderByDescending(x => x.VolunteerId);
                    break;
            }
        }
        else
            petsQuery = petsQuery.OrderBy(x => x.DateOfCreation);


        return await petsQuery.ToPagedListAsync(query.Page, query.PageSize, token);
    }
}
