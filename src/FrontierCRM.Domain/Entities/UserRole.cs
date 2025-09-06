using System.ComponentModel.DataAnnotations;
using FrontierCRM.Domain.Common;

namespace FrontierCRM.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between users and roles
/// </summary>
public class UserRole : TenantScopedEntity
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid RoleId { get; set; }
    
    public DateTime AssignedAt { get; init; } = DateTime.UtcNow;
    
    public Guid AssignedBy { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    
    public UserRole() { }
    
    public UserRole(Guid userId, Guid roleId, Guid assignedBy, Guid tenantId) : base(tenantId)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedBy = assignedBy;
    }
}
