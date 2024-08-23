using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Modules;

namespace PetFamily.Infrastructure
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Volunteer> Modules => Set<Volunteer>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");
        }
    }
}
