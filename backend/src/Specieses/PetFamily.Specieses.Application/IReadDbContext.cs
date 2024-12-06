using PetFamily.Core.DTOs;

namespace PetFamily.Specieses.Application;
public interface IReadDbContext
{
    IQueryable<SpeciesDto> Specieses { get; }
    IQueryable<BreedDto> Breeds { get; }
}
