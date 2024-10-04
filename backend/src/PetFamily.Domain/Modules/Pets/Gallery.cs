using CSharpFunctionalExtensions;
using PetFamily.Domain.Modules.Pets;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public record Gallery
{
    private Gallery()
    {

    }

    private Gallery(IEnumerable<PetPhoto> value)
    {
        Value = value.ToList();
    }

    public IReadOnlyList<PetPhoto> Value { get; }
    
    public static Result<Gallery> Create(IEnumerable<PetPhoto> petPhoto)
    {
        return new Gallery(petPhoto);
    }
}
