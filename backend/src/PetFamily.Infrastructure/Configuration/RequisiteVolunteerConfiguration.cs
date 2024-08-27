using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Constants;

namespace PetFamily.Infrastructure.Configuration
{
   class RequisiteVolunteerConfiguration : IEntityTypeConfiguration<RequisiteVolunteer>
    {
        public void Configure(EntityTypeBuilder<RequisiteVolunteer> builder)
        {
            builder.ToTable("requisite volunteer"); //название таблицы

            builder.HasKey(m => m.Id); //указываем ключ

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        }

    }

}
