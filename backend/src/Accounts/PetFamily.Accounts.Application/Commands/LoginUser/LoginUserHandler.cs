using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Contracts.Responces;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.LoginUser;
public class LoginUserHandler(
    UserManager<User> userManager,
    ITokenProvider tokenProvider,
    ILogger<LoginUserHandler> logger) : ICommandHandler<LoginResponce, LoginUserCommand>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger = logger;

    public async Task<Result<LoginResponce, ErrorList>> Handle(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Errors.General.AlreadyExist().ToErrorList();

        var passwordVerification = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordVerification)
            return Errors.User.InvalidIdentity().ToErrorList();

        var accessToken = await _tokenProvider.GenerateAccessToken(user, cancellationToken);

        var refreshToken = await _tokenProvider.GenerateRefreshToken(user, cancellationToken);

        _logger.LogInformation("User {email} is logged in", command.Email);

        return new LoginResponce(accessToken.AccessToken,refreshToken);
    }
}
