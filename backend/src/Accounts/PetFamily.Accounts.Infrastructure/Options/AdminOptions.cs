namespace PetFamily.Accounts.Infrastructure.Options;

public class AdminOptions
{
    public const string ADMIN = "ADMIN";

    public string UserName { get; init; } = ADMIN;
    public string Email { get; init; } = ADMIN;
    public string Password { get; init; } = ADMIN;
}