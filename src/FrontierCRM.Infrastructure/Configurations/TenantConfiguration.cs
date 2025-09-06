using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FrontierCRM.Domain.Entities;

namespace FrontierCRM.Infrastructure.Configurations;

/// <summary>
/// Entity Framework configuration for Tenant entity
/// </summary>
public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Subdomain)
            .HasMaxLength(100);

        builder.Property(t => t.Website)
            .HasMaxLength(500);

        builder.Property(t => t.Industry)
            .HasMaxLength(100);

        builder.Property(t => t.Settings)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                v => System.Text.Json.JsonSerializer.Deserialize<Domain.ValueObjects.TenantSettings>(v, (System.Text.Json.JsonSerializerOptions?)null)!)
            .HasColumnType("jsonb");

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Indexes
        builder.HasIndex(t => t.Subdomain)
            .IsUnique()
            .HasFilter("\"Subdomain\" IS NOT NULL");

        builder.HasIndex(t => t.Name);
        builder.HasIndex(t => t.IsActive);
        builder.HasIndex(t => t.CreatedAt);

        // Relationships
        builder.HasMany(t => t.Users)
            .WithOne(u => u.Tenant)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Roles)
            .WithOne(r => r.Tenant)
            .HasForeignKey(r => r.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
