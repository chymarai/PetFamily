using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain;
using PetFamily.SharedKernel;

namespace PetFamily.Accounts.Infrastructure;
public class AccountsDbContext(IConfiguration configuration)
    : IdentityDbContext<User, Role, Guid>
{
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        modelBuilder.Entity<AdminAccount>()
            .HasOne(u => u.User)
            .WithOne()
            .HasForeignKey<AdminAccount>(a => a.UserId);

        modelBuilder.Entity<Role>()
            .ToTable("roles");

        modelBuilder.Entity<AdminAccount>()
            .ComplexProperty(a => a.FullName, tb =>
            {
                tb.Property(m => m.FirstName).IsRequired().HasColumnName("first_name");
                tb.Property(m => m.LastName).IsRequired().HasColumnName("last_name");
                tb.Property(m => m.SurName).IsRequired().HasColumnName("sur_name");
            });

        modelBuilder.Entity<VolunteerAccount>()
            .OwnsOne(m => m.SocialNetworkDetails, mb =>
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

        modelBuilder.Entity<VolunteerAccount>()
            .OwnsOne(m => m.RequisiteDetails, mb =>
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

        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");

        modelBuilder.Entity<RolePermission>()
            .ToTable("rolePermission");

        modelBuilder.Entity<Permission>()
            .ToTable("permissions");

        modelBuilder.Entity<Permission>()
            .HasIndex(p => p.Code)
            .IsUnique();

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r  => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        modelBuilder.HasDefaultSchema("accounts");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}
