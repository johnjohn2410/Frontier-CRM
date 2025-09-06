namespace FrontierCRM.Domain.Common;

/// <summary>
/// Interface for entities that are scoped to a tenant
/// </summary>
public interface ITenantScoped
{
    /// <summary>
    /// The tenant ID that this entity belongs to
    /// </summary>
    Guid TenantId { get; }
}
