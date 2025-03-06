using PetFamily.Accounts.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.DataModels;
public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken);
}
