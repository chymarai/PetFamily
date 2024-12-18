﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Specieses.Domain;

namespace PetFamily.Specieses.Infrastructure.Configurations.Write;
public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");



        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value))
            .IsRequired();

        builder.ComplexProperty(m => m.SpeciesName, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("species_name");
        });

        builder.HasMany(m => m.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
