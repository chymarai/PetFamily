using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Modules;

namespace PetFamily.Infrastructure
{
    internal class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private const string DATABASE = "Database";
        public DbSet<Volunteer> Modules => Set<Volunteer>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseUpperSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
