using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Read;
public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteer");

        builder.HasKey(m => m.Id);

        //builder.HasMany(p => p.Pets)
        //    .WithOne()
        //    .HasForeignKey(p => p.VolunteerId);
    }
}
