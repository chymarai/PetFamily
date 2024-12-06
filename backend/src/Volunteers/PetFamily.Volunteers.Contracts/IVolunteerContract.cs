using CSharpFunctionalExtensions;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Contracts;
public interface IVolunteerContract
{
    public Task<Result<PetDto, Error>> PetUsedSpecies(Guid speciesId, CancellationToken token = default);
    public Task<Result<PetDto, Error>> PetUsedBreed(Guid breedId, CancellationToken token = default);
}
