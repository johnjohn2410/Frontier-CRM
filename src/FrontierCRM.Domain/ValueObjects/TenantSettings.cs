using System.Text.Json;
using FrontierCRM.Domain.Common;

namespace FrontierCRM.Domain.ValueObjects;

/// <summary>
/// Settings and configuration for a tenant
/// </summary>
public class TenantSettings : ValueObject
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
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TimeZone;
        yield return Locale;
        yield return Currency;
        yield return MaxUsers;
        yield return MaxStorageBytes;
        yield return MaxApiCallsPerMonth;
        yield return AllowCustomFields;
        yield return AllowWorkflows;
        yield return AllowIntegrations;
        yield return CustomSettings?.RootElement.GetHashCode() ?? 0;
    }
}
