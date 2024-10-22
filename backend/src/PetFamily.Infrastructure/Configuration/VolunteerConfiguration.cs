using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.Aggregate;

namespace PetFamily.Infrastructure.Configuration;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
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

        builder.ComplexProperty(m => m.FullName, tb =>
        {
            tb.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("firstName");

            tb.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("lastName");

            tb.Property(m => m.SurName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("middleName");
        });

        builder.ComplexProperty(m => m.Email, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
        });

        builder.ComplexProperty(m => m.PhoneNumber, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("phoneNumber");
        });

        builder.ComplexProperty(m => m.Description, tb =>
        {
             tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("description");
        });

        builder.ComplexProperty(m => m.Experience, tb =>
        {
            tb.Property(b => b.Value)
               .IsRequired()
               .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
               .HasColumnName("experience");
        });

        builder.OwnsOne(m => m.SocialNetworkDetails, mb =>
        {
            mb.ToJson("social_network");

            mb.OwnsMany(mb => mb.Value, mbBuilder =>
            {
                mbBuilder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("social_network_name");

                mbBuilder.Property(p => p.Url)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("social_network_url");
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

        builder.HasMany(m => m.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade); //при удалении волонтера, удаляются все животные


        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
