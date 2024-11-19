using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs;
public class BreedDto
{
    public Guid Id { get; init; }
    public Guid SpeciesId { get; init; }
    public string BreedName { get; init; }
}
