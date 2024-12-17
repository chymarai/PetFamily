using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.RegisterUser;
public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(UserManager<User> userManager, ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command, CancellationToken token)
    {
        var user = new User()
        {
            UserName = command.UserName,
            Email = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User {userName} created", command.UserName);

            return Result.Success<ErrorList>();
        }

        var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

        return new ErrorList(errors);
    }
}
