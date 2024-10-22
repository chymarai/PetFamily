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
    private Breed(BreedId breedid, string breedName) : base(breedid)
    {
        BreedName = breedName;
    }

    public string BreedName { get; }

    public static Result<Breed, Error> Create(BreedId breedid, string breedName)
    {
        if (string.IsNullOrWhiteSpace(breedName) || breedName.Length > Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        return new Breed(breedid, breedName);
    }
}