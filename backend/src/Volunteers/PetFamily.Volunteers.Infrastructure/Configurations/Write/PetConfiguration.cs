﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.Ids;
using PetFamily.Volunteers.Domain;
using PetFamily.Core.Extensions;
using PetFamily.Core.DTOs;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Write;

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
                .HasColumnName("color");
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
                .HasColumnName("weight");
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

        builder.Property(p => p.Files)
             .ValueObjectsCollectionJsonConversion(
                 file => new PetFileDto { PathToStorage = file.PathToStorage.Path },
                 dto => new PetFiles(FilePath.Create(dto.PathToStorage).Value))
             .HasColumnName("files");

        builder.Property<bool>("IsDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
