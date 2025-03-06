using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Application.DataModels;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.LoginUser;
public class LoginUserHandler(
    UserManager<User> userManager,
    ITokenProvider tokenProvider,
    ILogger<LoginUserHandler> logger) : ICommandHandler<JwtTokenResult, LoginUserCommand>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger = logger;

    public async Task<Result<JwtTokenResult, ErrorList>> Handle(LoginUserCommand command, CancellationToken token = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Errors.General.AlreadyExist().ToErrorList();

        var passwordVerification = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordVerification)
            return Errors.User.InvalidIdentity().ToErrorList();

        var jwtToken = await _tokenProvider.GenerateAccessToken(user, token);

        _logger.LogInformation("User {email} is logged in", command.Email);

        return jwtToken;
    }
}
