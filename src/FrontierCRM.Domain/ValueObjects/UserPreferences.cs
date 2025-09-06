using System.Text.Json;
using FrontierCRM.Domain.Common;

namespace FrontierCRM.Domain.ValueObjects;

/// <summary>
/// User preferences and settings
/// </summary>
public class UserPreferences : ValueObject
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
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TimeZone;
        yield return Locale;
        yield return DateFormat;
        yield return TimeFormat;
        yield return Currency;
        yield return EmailNotifications;
        yield return PushNotifications;
        yield return DesktopNotifications;
        yield return ItemsPerPage;
        yield return Theme;
        yield return CustomPreferences?.RootElement.GetHashCode() ?? 0;
    }
}
