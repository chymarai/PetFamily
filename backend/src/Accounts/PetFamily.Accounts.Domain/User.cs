using Microsoft.AspNetCore.Identity;

namespace PetFamily.Accounts.Domain;
public class User : IdentityUser<Guid>
{
    public User()
    {
    }
    private List<Role> _roles = [];
    public IReadOnlyList<Role> Roles => _roles;

    public static User CreateAdmin(string username, string email, Role role)
    {
        return new User
        {
            UserName = username,
            Email = email,
            _roles = [role]
        };
    }
}
