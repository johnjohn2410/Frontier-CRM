using MediatR;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Tenants.Queries.GetTenant;

/// <summary>
/// Query to get a tenant by ID
/// </summary>
public record GetTenantQuery(Guid Id) : IRequest<TenantDto?>;
