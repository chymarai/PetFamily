using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain;
public class VolunteerAccount
{
    public Guid Id { get; set; }
    public RequisiteDetails RequisiteDetails { get; set; } = default!;
    public SocialNetworkDetails SocialNetworkDetails { get; set; } = default!;
}
