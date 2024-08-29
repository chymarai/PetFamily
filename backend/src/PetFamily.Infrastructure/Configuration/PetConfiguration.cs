using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Constants;

namespace PetFamily.Infrastructure.Configuration
{
    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet"); 

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.Breed)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.HealthInformation)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
           
            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.Weight)
                .IsRequired();

            builder.Property(m => m.Height)
                .IsRequired();

            builder.Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.IsCastrated)
               .IsRequired();

            builder.Property(m => m.IsVaccination)
               .IsRequired();

            builder.Property(m => m.AssistanceStatus)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.BirthDate)
               .IsRequired();

            builder.Property(m => m.DateOfCreation)
               .IsRequired();

            builder.HasMany(m => m.Requisite)
                .WithOne();
                
            builder.HasMany(m => m.PetPhoto)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
