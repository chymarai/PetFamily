using Microsoft.AspNetCore.Authorization;

namespace PetFamily.Framework.Authorization;
public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    public PermissionRequirementHandler()
    {
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permission)
    {
        //проверяем что у пользователя есть нужные разрешения
        var permissionUser = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
        if (permissionUser == null)
            return;

        if (permissionUser.Value == permission.Code)
        {
            context.Succeed(permission);
        }
    }
}
