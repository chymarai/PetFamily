using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Infrastructure.IdentityManagers;
using PetFamily.Accounts.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetFamily.Accounts.Infrastructure;
public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly PermissionManager _permissionManager;
    private readonly AccountsDbContext _accountsDbContext;
    private readonly RefreshTokenOptions _refreshTokenOptions;

    public JwtTokenProvider(
        IOptions<JwtOptions> options,
        PermissionManager permissionManager,
        AccountsDbContext accountsDbContext,
        RefreshTokenOptions refreshTokenOptions)
    {
        _jwtOptions = options.Value;
        _permissionManager = permissionManager;
        _accountsDbContext = accountsDbContext;
        _refreshTokenOptions = refreshTokenOptions;
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

        var jti = Guid.NewGuid();

        Claim[] claims =
        [
            new(CustomClaims.Id, user.Id.ToString()),
            new(CustomClaims.Jti, jti.ToString()),
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

    public async Task<Guid> GenerateRefreshToken(User user, CancellationToken cancellationToken)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            CreateAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenOptions.ExpiredDaysTime)),
            RefreshToken = Guid.NewGuid()
        };

        _accountsDbContext.Add(refreshSession);

        await _accountsDbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
    }
}
