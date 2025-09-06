using MediatR;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Tenants.Commands.CreateTenant;

/// <summary>
/// Command to create a new tenant
/// </summary>
public record CreateTenantCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string? Subdomain { get; init; }
    public string? Website { get; init; }
    public string? Industry { get; init; }
    public TenantSettingsDto Settings { get; init; } = new();
}
