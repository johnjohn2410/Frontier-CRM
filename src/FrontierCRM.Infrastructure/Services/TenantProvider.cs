using Microsoft.AspNetCore.Http;
using FrontierCRM.Application.Common.Interfaces;

namespace FrontierCRM.Infrastructure.Services;

/// <summary>
/// Implementation of ITenantProvider that resolves tenant from HTTP context
/// </summary>
public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Guid? _cachedTenantId;
    private string? _cachedTenantName;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            if (_cachedTenantId.HasValue)
                return _cachedTenantId.Value;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Items.TryGetValue("TenantId", out var tenantIdObj) == true && 
                tenantIdObj is Guid tenantId)
            {
                _cachedTenantId = tenantId;
                return tenantId;
            }

            throw new InvalidOperationException("No tenant context available. Ensure tenant middleware is configured.");
        }
    }

    public string? TenantName
    {
        get
        {
            if (_cachedTenantName != null)
                return _cachedTenantName;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Items.TryGetValue("TenantName", out var tenantNameObj) == true && 
                tenantNameObj is string tenantName)
            {
                _cachedTenantName = tenantName;
                return tenantName;
            }

            return null;
        }
    }

    public bool HasTenant
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext?.Items.ContainsKey("TenantId") == true;
        }
    }
}
