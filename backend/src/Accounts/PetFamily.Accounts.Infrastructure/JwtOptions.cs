﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure;
public class JwtOptions
{
    public const string JWT = nameof(JWT);
    public string Issuer { get; init; } 
    public string Audience { get; init; }
    public string Key{ get; init; }
    public string ExpiredMinutesTime{ get; init; }
}