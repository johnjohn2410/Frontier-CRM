using Microsoft.EntityFrameworkCore;
using FrontierCRM.Application.Common.Interfaces;

namespace FrontierCRM.Infrastructure.Middleware;

/// <summary>
/// Middleware for resolving tenant context from HTTP requests
/// </summary>
public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantMiddleware> _logger;

    public TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IApplicationDbContext dbContext)
    {
        try
        {
            var tenantId = await ResolveTenantAsync(context, dbContext);
            
            if (tenantId != Guid.Empty)
            {
                context.Items["TenantId"] = tenantId;
                
                // Get tenant name for logging/debugging
                var tenant = await dbContext.Tenants
                    .Where(t => t.Id == tenantId)
                    .Select(t => new { t.Name, t.Subdomain })
                    .FirstOrDefaultAsync();
                
                if (tenant != null)
                {
                    context.Items["TenantName"] = tenant.Name;
                    context.Items["TenantSubdomain"] = tenant.Subdomain;
                }
            }
            else
            {
                _logger.LogWarning("No tenant resolved for request to {Path}", context.Request.Path);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resolving tenant for request to {Path}", context.Request.Path);
        }

        await _next(context);
    }

    private async Task<Guid> ResolveTenantAsync(HttpContext context, IApplicationDbContext dbContext)
    {
        // Method 1: Check X-Tenant-Id header
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantIdHeader))
        {
            if (Guid.TryParse(tenantIdHeader.FirstOrDefault(), out var tenantId))
            {
                return await ValidateTenantAsync(tenantId, dbContext);
            }
        }

        // Method 2: Check subdomain
        var host = context.Request.Host.Host;
        if (!string.IsNullOrEmpty(host))
        {
            var subdomain = ExtractSubdomain(host);
            if (!string.IsNullOrEmpty(subdomain))
            {
                var tenant = await dbContext.Tenants
                    .Where(t => t.Subdomain == subdomain && t.IsActive)
                    .Select(t => t.Id)
                    .FirstOrDefaultAsync();
                
                if (tenant != Guid.Empty)
                {
                    return tenant;
                }
            }
        }

        // Method 3: Use default tenant (for development/testing)
        var defaultTenant = await dbContext.Tenants
            .Where(t => t.Subdomain == "default" && t.IsActive)
            .Select(t => t.Id)
            .FirstOrDefaultAsync();

        return defaultTenant;
    }

    private async Task<Guid> ValidateTenantAsync(Guid tenantId, IApplicationDbContext dbContext)
    {
        var exists = await dbContext.Tenants
            .Where(t => t.Id == tenantId && t.IsActive)
            .AnyAsync();
        
        return exists ? tenantId : Guid.Empty;
    }

    private static string? ExtractSubdomain(string host)
    {
        var parts = host.Split('.');
        if (parts.Length > 2)
        {
            return parts[0];
        }
        return null;
    }
}
