using System.ComponentModel.DataAnnotations;
using FrontierCRM.Domain.Common;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Domain.Entities;

/// <summary>
/// Represents a user in the CRM system
/// </summary>
public class User : TenantScopedEntity
{
    [Required]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string DisplayName { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string? Phone { get; set; }
    
    [MaxLength(200)]
    public string? JobTitle { get; set; }
    
    [MaxLength(100)]
    public string? Department { get; set; }
    
    public UserPreferences Preferences { get; set; } = new();
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public DateTime? LastLoginAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public bool IsEmailVerified { get; set; } = false;
    
    // Navigation properties
    public Tenant Tenant { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    
    public User() { }
    
    public User(Guid tenantId, string email, string firstName, string lastName) : base(tenantId)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DisplayName = $"{firstName} {lastName}".Trim();
    }
    
    public void UpdateProfile(string firstName, string lastName, string? phone = null, string? jobTitle = null, string? department = null)
    {
        FirstName = firstName;
        LastName = lastName;
        DisplayName = $"{firstName} {lastName}".Trim();
        Phone = phone;
        JobTitle = jobTitle;
        Department = department;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdatePreferences(UserPreferences preferences)
    {
        Preferences = preferences;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
