using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FrontierCRM.Domain.Entities;

namespace FrontierCRM.Infrastructure.Configurations;

/// <summary>
/// Entity Framework configuration for UserRole entity
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => ur.Id);

        builder.Property(ur => ur.TenantId)
            .IsRequired();

        builder.Property(ur => ur.UserId)
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .IsRequired();

        builder.Property(ur => ur.AssignedAt)
            .IsRequired();

        builder.Property(ur => ur.AssignedBy)
            .IsRequired();

        // Indexes
        builder.HasIndex(ur => new { ur.TenantId, ur.UserId, ur.RoleId })
            .IsUnique();

        builder.HasIndex(ur => ur.TenantId);
        builder.HasIndex(ur => ur.UserId);
        builder.HasIndex(ur => ur.RoleId);

        // Relationships
        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
