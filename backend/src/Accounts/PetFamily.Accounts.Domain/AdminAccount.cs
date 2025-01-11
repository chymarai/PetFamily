using PetFamily.Volunteers.Domain.VolunteersValueObjects;

namespace PetFamily.Accounts.Domain;

public class AdminAccount
{
    public const string ADMIN = nameof(ADMIN);

    private AdminAccount()
    {
    }

    public static AdminAccount Create(User user, FullName fullName)
    {
        return new AdminAccount
        {
            Id = Guid.NewGuid(),
            User = user,
            FullName = fullName,
        };
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public FullName FullName { get; set; }
}
