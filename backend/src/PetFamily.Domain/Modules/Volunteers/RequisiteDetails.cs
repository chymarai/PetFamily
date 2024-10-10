using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Modules.Volunteers;

public record RequisiteDetails
{
    private RequisiteDetails()
    {

    }

    private RequisiteDetails(IEnumerable<Requisite> value)
    {
        Value = value.ToList();
    }

    public IReadOnlyList<Requisite> Value { get; }

    public static RequisiteDetails Create(IEnumerable<Requisite> value)
    {
        return new RequisiteDetails(value);
    }
}
