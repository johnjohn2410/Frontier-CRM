namespace FrontierCRM.Application.Common.Interfaces;

/// <summary>
/// Service for accessing current date and time
/// </summary>
public interface IDateTime
{
    /// <summary>
    /// Gets the current UTC date and time
    /// </summary>
    DateTime UtcNow { get; }
    
    /// <summary>
    /// Gets the current date and time in the specified timezone
    /// </summary>
    /// <param name="timeZoneId">The timezone ID</param>
    /// <returns>Current date and time in the specified timezone</returns>
    DateTime NowInTimeZone(string timeZoneId);
}
