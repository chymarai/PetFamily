using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Modules.Pets;

namespace PetFamily.Infrastructure.Configuration
{
    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet"); 

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value))
                .IsRequired();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.ComplexProperty(m => m.speciesBreedObject, tb =>
            {
                tb.Property(sbo => sbo.SpeciesId)
                    .HasConversion(
                        speciesId => speciesId.Value,
                        value => SpeciesId.Create(value))
                    .IsRequired();
                tb.Property(sbo => sbo.BreedId)
                    .IsRequired();
            });

            builder.Property(m => m.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.HealthInformation)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.ComplexProperty(m => m.Address, tb =>
            {
                tb.Property(m => m.Country)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("country");

                tb.Property(m => m.Region)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("region");

                tb.Property(m => m.City)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("city");

                tb.Property(m => m.Street)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("street");
            });
            
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

            builder.OwnsOne(m => m.RequisiteDetails, mb =>
            {
                mb.ToJson("requisite");

                mb.OwnsMany(mb => mb.Value, mbBuilder =>
                {
                    mbBuilder.Property(p => p.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    mbBuilder.Property(p => p.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.OwnsOne(m => m.Gallery, mb =>
            {
                mb.ToJson("Gallery");

                mb.OwnsMany(mb => mb.Value, mbBuilder =>
                {
                    mbBuilder.Property(p => p.Storage)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    mbBuilder.Property(p => p.IsMain)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });
        }
    }
}
