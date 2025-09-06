using FrontierCRM.Application.Common.Interfaces;

namespace FrontierCRM.Application.Common.Services;

/// <summary>
/// Implementation of IDateTime service
/// </summary>
public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime NowInTimeZone(string timeZoneId)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(UtcNow, timeZone);
    }
}
