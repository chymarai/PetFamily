using System.Data;

namespace PetFamily.Volunteers.Application;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}