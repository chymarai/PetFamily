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
    internal class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteer");
            
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id);

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
            
            builder.HasMany(x => x.SocialNetworks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Requisite)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Pets)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
