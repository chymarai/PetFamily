using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Contracts.Responces;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.RefreshToken;

public class RefreshTokenHandler : ICommandHandler<RefreshTokenResponce, RefreshTokenCommand>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly UserManager<User> _userManager;

    public RefreshTokenHandler(
        ITokenProvider tokenProvider,
        IRefreshSessionManager refreshSessionManager,
        UserManager<User> userManager)
    {
        _tokenProvider = tokenProvider;
        _refreshSessionManager = refreshSessionManager;
        _userManager = userManager;
    }
    public async Task<Result<RefreshTokenResponce, ErrorList>> Handle(
        RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenVerification = await _refreshSessionManager.GetByRefreshToken(command.RefreshToken, cancellationToken);

        var userId = tokenVerification.Value.UserId;

        var user = _userManager.FindByIdAsync(userId.ToString()).Result;
        if (user == null)
            return Errors.General.NotFound().ToErrorList();

        var accessToken = await _tokenProvider.GenerateAccessToken(user, cancellationToken);

        var refreshToken = await _tokenProvider.GenerateRefreshToken(user, cancellationToken);

        return new RefreshTokenResponce(accessToken.ToString(), refreshToken);
    }
}