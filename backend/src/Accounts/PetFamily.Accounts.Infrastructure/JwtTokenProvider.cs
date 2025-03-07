using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Application.DataModels;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Framework.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure;
public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly PermissionManager _permissionManager;
    private readonly AccountsDbContext _accountsDbContext;

    public JwtTokenProvider(
        IOptions<JwtOptions> options,
        PermissionManager permissionManager,
        AccountsDbContext accountsDbContext)
    {
        _jwtOptions = options.Value;
        _permissionManager = permissionManager;
        _accountsDbContext = accountsDbContext;
    }
    public async Task<JwtTokenResult> GenerateAccessToken(
        User user,
        CancellationToken cancellationToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roleClaims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name ?? string.Empty));

        var permissions = await _permissionManager.GetUserPermissionCodes(user.Id, cancellationToken);
        var permissionsClaims = permissions.Select(r => new Claim(CustomClaims.Permission, r ?? string.Empty));

        Claim[] claims =
        [
            new(CustomClaims.Id, user.Id.ToString()),
            new(CustomClaims.Email, user.Email!)
        ];

        claims = claims.Union(roleClaims).Union(permissionsClaims).ToArray();

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtOptions.ExpiredMinutesTime)),
            signingCredentials: signingCredentials,
            claims: claims
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokenResult(token);
    }
}
