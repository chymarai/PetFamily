using PetFamily.Core.Abstraction;

namespace PetFamily.Accounts.Application.Commands.RegisterUser;
public record RegisterUserCommand(
    string UserName,
    string Email,
    string Password) : ICommand;
