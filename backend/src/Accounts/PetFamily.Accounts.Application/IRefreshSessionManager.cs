using CSharpFunctionalExtensions;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application;

public interface IRefreshSessionManager
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
}
