using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain;
public class RolePermission
{
    public Guid RolePermissionId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid RoleId { get; set; }
}
