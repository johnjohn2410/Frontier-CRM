using System.ComponentModel.DataAnnotations;
using FrontierCRM.Domain.Common;

namespace FrontierCRM.Domain.Entities;

/// <summary>
/// Represents a role in the CRM system
/// </summary>
public class Role : TenantScopedEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public bool IsSystemRole { get; set; } = false;
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public Tenant Tenant { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    
    public Role() { }
    
    public Role(Guid tenantId, string name, string? description = null, bool isSystemRole = false) : base(tenantId)
    {
        Name = name;
        Description = description;
        IsSystemRole = isSystemRole;
    }
    
    public void UpdateDetails(string name, string? description = null)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
