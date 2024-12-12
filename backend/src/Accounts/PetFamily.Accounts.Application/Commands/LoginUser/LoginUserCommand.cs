using PetFamily.Core.Abstraction;

namespace PetFamily.Accounts.Application.Commands.LoginUser;
public record LoginUserCommand(
    string Email,
    string Password) : ICommand;
