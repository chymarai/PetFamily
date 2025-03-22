using PetFamily.Core.Abstraction;
using System;

namespace PetFamily.Accounts.Application.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, Guid RefreshToken) : ICommand;