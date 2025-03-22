using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Options;
public class RefreshTokenOptions
{
    public const string RefreshSession = nameof(RefreshSession);
    public string ExpiredDaysTime { get; init; }
}
