using CSharpFunctionalExtensions;

namespace PetFamily.Volunteers.Domain.VolunteersValueObjects;

public record SocialNetworkDetails
{
    private SocialNetworkDetails()
    {

    }

    public SocialNetworkDetails(IEnumerable<SocialNetwork> value)
    {
        Value = value.ToList();
    }

    public IReadOnlyList<SocialNetwork> Value { get; }

    public static SocialNetworkDetails Create(IEnumerable<SocialNetwork> value)
    {
        return new SocialNetworkDetails(value);
    }
}
