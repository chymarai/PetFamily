using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Accounts.Infrastructure;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public PermissionAttribute(string code) : base(policy: code)
    {
        Code = code;
    }

    public string Code { get; set; }
}