using PetFamily.Accounts.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application;
public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);

    Task<Guid> GenerateRefreshToken(User user, CancellationToken cancellationToken);
}
