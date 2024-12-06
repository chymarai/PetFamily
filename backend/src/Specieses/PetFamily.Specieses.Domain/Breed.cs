using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;

namespace PetFamily.Specieses.Domain;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {

    }
    public Breed(BreedId id, BreedName breedName) : base(id)
    {
        BreedName = breedName;
    }

    public BreedName BreedName { get; } = default!;
}