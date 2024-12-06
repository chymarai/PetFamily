using CSharpFunctionalExtensions;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Specieses.Contracts;
public interface ISpeciesContract
{
    public Task<Result<SpeciesDto, Error>> SpeciesExist(Guid speciesId, CancellationToken token); 

    public Task<Result<BreedDto, Error>> BreedExists(Guid breedId, CancellationToken token);
}
