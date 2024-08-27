using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Constants;
using PetFamily.Domain.Modules;

namespace PetFamily.Infrastructure.Configuration
{
    internal class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("petphoto"); //название таблицы

            builder.HasKey(m => m.Id); //указываем ключ

            builder.Property(m => m.Storage)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.IsMain)
                .IsRequired();
        }


    }
}
