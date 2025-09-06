using System.Text.Json;

namespace FrontierCRM.Application.Common.DTOs;

/// <summary>
/// Data Transfer Object for User
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? JobTitle { get; set; }
    public string? Department { get; set; }
    public UserPreferencesDto Preferences { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsEmailVerified { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}

/// <summary>
/// Data Transfer Object for User Preferences
/// </summary>
public class UserPreferencesDto
{
    public string TimeZone { get; set; } = "UTC";
    public string Locale { get; set; } = "en-US";
    public string DateFormat { get; set; } = "MM/dd/yyyy";
    public string TimeFormat { get; set; } = "12h";
    public string Currency { get; set; } = "USD";
    public bool EmailNotifications { get; set; } = true;
    public bool PushNotifications { get; set; } = true;
    public bool DesktopNotifications { get; set; } = true;
    public int ItemsPerPage { get; set; } = 25;
    public string Theme { get; set; } = "light";
    public JsonDocument? CustomPreferences { get; set; }
}
