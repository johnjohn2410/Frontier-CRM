using System.ComponentModel.DataAnnotations;

namespace FrontierCRM.Domain.Common;

/// <summary>
/// Base entity class for entities that are scoped to a tenant
/// </summary>
public abstract class TenantScopedEntity : Entity, ITenantScoped
{
    /// <summary>
    /// The tenant ID that this entity belongs to
    /// </summary>
    [Required]
    public Guid TenantId { get; init; }
    
    protected TenantScopedEntity() { }
    
    protected TenantScopedEntity(Guid tenantId)
    {
        TenantId = tenantId;
    }
}
