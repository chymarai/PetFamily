using PetFamily.Accounts.Application.Commands.RefreshToken;

namespace PetFamily.Accounts.Presentation.Requests;

public record RefreshTokenRequest(string AccessToken, Guid RefreshToken)
{
    public RefreshTokenCommand ToCommand() => new(AccessToken, RefreshToken);
}