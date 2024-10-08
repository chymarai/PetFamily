using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Pets;

public record BreedId
{
    public BreedId()
    {

    }
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId NewVolunteerId() => new(Guid.NewGuid());
    public static BreedId Empty() => new(Guid.Empty);
    public static BreedId Create(Guid id) => new(id);
}