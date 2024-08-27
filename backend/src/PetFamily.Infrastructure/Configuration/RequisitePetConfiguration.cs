using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Modules;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Constants;

namespace PetFamily.Infrastructure.Configuration
{
    internal class RequisitePetConfiguration : IEntityTypeConfiguration<RequisitePet>
    {
        public void Configure(EntityTypeBuilder<RequisitePet> builder)
        {

        }

    }
}
