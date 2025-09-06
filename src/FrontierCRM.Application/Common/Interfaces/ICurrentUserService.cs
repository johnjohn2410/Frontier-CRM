namespace FrontierCRM.Application.Common.Interfaces;

/// <summary>
/// Service for accessing current user information
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user ID
    /// </summary>
    Guid? UserId { get; }
    
    /// <summary>
    /// Gets the current user email
    /// </summary>
    string? Email { get; }
    
    /// <summary>
    /// Gets the current user display name
    /// </summary>
    string? DisplayName { get; }
    
    /// <summary>
    /// Checks if a user is authenticated
    /// </summary>
    bool IsAuthenticated { get; }
    
    /// <summary>
    /// Gets the current user's roles
    /// </summary>
    IReadOnlyList<string> Roles { get; }
}
