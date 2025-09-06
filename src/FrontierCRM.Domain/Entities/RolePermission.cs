using System.ComponentModel.DataAnnotations;
using FrontierCRM.Domain.Common;

namespace FrontierCRM.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between roles and permissions
/// </summary>
public class RolePermission : TenantScopedEntity
{
    [Required]
    public Guid RoleId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Permission { get; set; } = string.Empty;
    
    public DateTime AssignedAt { get; init; } = DateTime.UtcNow;
    
    public Guid AssignedBy { get; set; }
    
    // Navigation properties
    public Role Role { get; set; } = null!;
    
    public RolePermission() { }
    
    public RolePermission(Guid roleId, string permission, Guid assignedBy, Guid tenantId) : base(tenantId)
    {
        RoleId = roleId;
        Permission = permission;
        AssignedBy = assignedBy;
    }
}
