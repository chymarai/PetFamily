using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetFamily.Domain.SpeciesManagment;

public class Breed : Shared.Entity<BreedId>
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