using System.Text.Json;

namespace FrontierCRM.Application.Common.DTOs;

/// <summary>
/// Data Transfer Object for Tenant
/// </summary>
public class TenantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Subdomain { get; set; }
    public string? Website { get; set; }
    public string? Industry { get; set; }
    public TenantSettingsDto Settings { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// Data Transfer Object for Tenant Settings
/// </summary>
public class TenantSettingsDto
{
    public string TimeZone { get; set; } = "UTC";
    public string Locale { get; set; } = "en-US";
    public string Currency { get; set; } = "USD";
    public int MaxUsers { get; set; } = 10;
    public long MaxStorageBytes { get; set; } = 1024 * 1024 * 1024; // 1GB
    public int MaxApiCallsPerMonth { get; set; } = 10000;
    public bool AllowCustomFields { get; set; } = true;
    public bool AllowWorkflows { get; set; } = false;
    public bool AllowIntegrations { get; set; } = false;
    public JsonDocument? CustomSettings { get; set; }
}
