using PetFamily.Accounts.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.DataModels;
public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}
