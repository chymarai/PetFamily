using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace PetFamily.Framework.Authorization;
public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permissionAttribute)
    {
        //проверяем что у пользователя есть нужные разрешения

        var permissions = context.User.Claims
            .Where(claim => claim.Type == CustomClaims.Permission)
            .Select(claim => claim.Value)
            .ToList();

        if (permissions.Contains(permissionAttribute.Code))
        {
            context.Succeed(permissionAttribute);
            return;
        }
        context.Fail();
    }
}
