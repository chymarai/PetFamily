﻿using Microsoft.EntityFrameworkCore;
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
    internal class RequisitePetConfiguration : IEntityTypeConfiguration<RequisitePet>
    {
        public void Configure(EntityTypeBuilder<RequisitePet> builder)
        {
            builder.ToTable("petphoto"); 

            builder.HasNoKey();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        }

    }

    
}
