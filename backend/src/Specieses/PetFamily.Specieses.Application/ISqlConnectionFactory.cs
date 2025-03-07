using System.Data;

namespace PetFamily.Specieses.Application;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}