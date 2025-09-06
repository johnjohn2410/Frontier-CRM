namespace FrontierCRM.Application.Common.Interfaces;

/// <summary>
/// Provides access to the current tenant context
/// </summary>
public interface ITenantProvider
{
    /// <summary>
    /// Gets the current tenant ID
    /// </summary>
    Guid TenantId { get; }
    
    /// <summary>
    /// Gets the current tenant name
    /// </summary>
    string? TenantName { get; }
    
    /// <summary>
    /// Checks if a tenant context is available
    /// </summary>
    bool HasTenant { get; }
}
