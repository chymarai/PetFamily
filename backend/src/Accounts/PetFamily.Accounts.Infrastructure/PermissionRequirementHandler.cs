using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure;
public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    public PermissionRequirementHandler()
    {
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permission)
    {
        var permissionUser = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
        if (permissionUser == null)
            return;

        if (permissionUser.Value == permission.Code)
        {
            context.Succeed(permission);
        }
    }
}
