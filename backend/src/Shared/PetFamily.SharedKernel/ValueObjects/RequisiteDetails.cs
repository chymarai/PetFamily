using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;

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
