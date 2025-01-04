using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain;
public class Role : IdentityRole<Guid>
{
    public Guid RoleId { get; set; } 
    public string Description { get; set; } = string.Empty;
    public List<RolePermission> RolePermissions { get; set; } = [];
}
