using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Modules.Volunteers;

namespace PetFamily.Infrastructure.Configuration
{
    internal class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteer");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value))
                .IsRequired();

            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(m => m.YearsOfExperience)
                .IsRequired();

            builder.Property(m => m.CountOfShelterAnimals)
                .IsRequired();

            builder.Property(m => m.CountOfHomelessAnimals)
                .IsRequired();

            builder.Property(m => m.CountOfIllAnimals)
                .IsRequired();

            builder.Property(m => m.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.OwnsOne(m => m.SocialNetworkDetails, mb =>
            {
                mb.ToJson("social_network");

                mb.OwnsMany(mb => mb.Value, mbBuilder =>
                {
                    mbBuilder.Property(p => p.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    mbBuilder.Property(p => p.Url)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

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

            builder.HasMany(m => m.Pet)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
