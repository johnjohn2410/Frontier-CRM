namespace FrontierCRM.Application.Common.DTOs;

/// <summary>
/// Data Transfer Object for Role
/// </summary>
public class RoleDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsSystemRole { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IList<string> Permissions { get; set; } = new List<string>();
}
