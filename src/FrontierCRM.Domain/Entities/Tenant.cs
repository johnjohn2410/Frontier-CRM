using System.ComponentModel.DataAnnotations;
using FrontierCRM.Domain.Common;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Domain.Entities;

/// <summary>
/// Represents a tenant in the multi-tenant CRM system
/// </summary>
public class Tenant : Entity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Subdomain { get; set; }
    
    [MaxLength(500)]
    public string? Website { get; set; }
    
    [MaxLength(100)]
    public string? Industry { get; set; }
    
    public TenantSettings Settings { get; set; } = new();
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public Tenant() { }
    
    public Tenant(string name, string? subdomain = null)
    {
        Name = name;
        Subdomain = subdomain;
    }
    
    public void UpdateSettings(TenantSettings settings)
    {
        Settings = settings;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
