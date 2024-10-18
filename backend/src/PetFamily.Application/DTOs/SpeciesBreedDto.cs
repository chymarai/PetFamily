using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs
{
    public record SpeciesBreedDto(Guid SpeciesId, Guid BreedId);
}
