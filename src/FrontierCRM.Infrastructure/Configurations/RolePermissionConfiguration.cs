using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FrontierCRM.Domain.Entities;

namespace FrontierCRM.Infrastructure.Configurations;

/// <summary>
/// Entity Framework configuration for RolePermission entity
/// </summary>
public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.TenantId)
            .IsRequired();

        builder.Property(rp => rp.RoleId)
            .IsRequired();

        builder.Property(rp => rp.Permission)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rp => rp.AssignedAt)
            .IsRequired();

        builder.Property(rp => rp.AssignedBy)
            .IsRequired();

        // Indexes
        builder.HasIndex(rp => new { rp.TenantId, rp.RoleId, rp.Permission })
            .IsUnique();

        builder.HasIndex(rp => rp.TenantId);
        builder.HasIndex(rp => rp.RoleId);
        builder.HasIndex(rp => rp.Permission);

        // Relationships
        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
