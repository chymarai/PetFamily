using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Domain;


namespace PetFamily.Specieses.Application;

public interface ISpeciesesRepository
{
    Task<Guid> Add(Species species, CancellationToken cancellationToken = default);
    Guid Delete(Species species, CancellationToken cancellationToken = default);
    Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken token = default);
    Guid Save(Species species, CancellationToken cancellationToken = default);
}