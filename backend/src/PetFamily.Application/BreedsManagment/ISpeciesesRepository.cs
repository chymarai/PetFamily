using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagment;

namespace PetFamily.Application.Specieses;

public interface ISpeciesesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);
    Guid Delete(Species species, CancellationToken cancellationToken = default);
    Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken token = default);
    Guid Save(Species species, CancellationToken cancellationToken = default);
}