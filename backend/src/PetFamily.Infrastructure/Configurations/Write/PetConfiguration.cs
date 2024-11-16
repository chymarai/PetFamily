using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Modules;
using PetFamily.Domain.Shared;
using PetFamily.Domain.PetsManagment.Ids;
using PetFamily.Domain.PetsManagment.Entities;
using PetFamily.Domain.SpeciesManagment;
using PetFamily.Domain.PetsManagment.ValueObjects.Pets;

namespace PetFamily.Infrastructure.Configurations.Write;

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

        builder.ComplexProperty(m => m.Name, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
        });

        builder.ComplexProperty(p => p.SpeciesBreed, pb =>
        {
            pb.Property(s => s.SpeciesId)
            .HasConversion(
                    s => s.Value,
                    v => SpeciesId.Create(v))
            .IsRequired()
            .HasColumnName("species_id");

            pb.Property(b => b.BreedId)
            .IsRequired()
            .HasColumnName("breed_id");
        });

        builder.ComplexProperty(m => m.Description, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("description");
        });

        builder.ComplexProperty(m => m.Position, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("position");
        });

        builder.ComplexProperty(m => m.Color, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
        });

        builder.ComplexProperty(m => m.HealthInformation, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("health_information");
        });

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

        builder.ComplexProperty(m => m.Weight, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("height");
        });

        builder.ComplexProperty(m => m.Height, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("height");
        });

        builder.ComplexProperty(m => m.PhoneNumber, tb =>
        {
            tb.Property(b => b.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.Property(m => m.IsCastrated)
           .IsRequired();

        builder.Property(m => m.IsVaccination)
           .IsRequired();

        builder.Property(m => m.AssistanceStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.ComplexProperty(m => m.BirthDate, mb =>
        {
            mb.Property(b => b.Value)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired()
                .HasColumnName("birthdate");
        });

        builder.Property(m => m.DateOfCreation)
           .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
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

        builder.OwnsOne(m => m.Files, mb =>
        {
            mb.ToJson("gallery");

            mb.OwnsMany(mb => mb.Values, mbBuilder =>
            {
                mbBuilder.Property(p => p.PathToStorage)
                    .HasConversion(
                        p => p.Path,
                        value => FilePath.Create(value).Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
