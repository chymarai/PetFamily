using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Modules.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configuration
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(x => x.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                .IsRequired();

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.HasMany(x => x.Breeds)
                .WithOne(x => x.Species);
        }
    }
}