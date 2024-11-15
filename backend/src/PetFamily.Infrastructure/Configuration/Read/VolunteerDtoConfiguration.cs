using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.DTOs;
using PetFamily.Domain.Modules.Volunteers;
using PetFamily.Domain.PetsManagment.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configuration.Read;
public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteer");

        builder.HasKey(m => m.Id);

        builder.HasMany(p => p.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);
    }
}
