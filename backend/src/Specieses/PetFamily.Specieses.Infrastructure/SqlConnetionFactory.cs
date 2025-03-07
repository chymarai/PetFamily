using Microsoft.Extensions.Configuration;
using Npgsql;
using PetFamily.Specieses.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Specieses.Infrastructure;
public class SqlConnetionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnetionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IDbConnection Create() => new NpgsqlConnection(_configuration.GetConnectionString("Database"));
}
