using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FrontierCRM.Application.Common.Interfaces;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Tenants.Queries.GetTenant;

/// <summary>
/// Handler for getting a tenant by ID
/// </summary>
public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, TenantDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITenantProvider _tenantProvider;

    public GetTenantQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ITenantProvider tenantProvider)
    {
        _context = context;
        _mapper = mapper;
        _tenantProvider = tenantProvider;
    }

    public async Task<TenantDto?> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _context.Tenants
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        return tenant == null ? null : _mapper.Map<TenantDto>(tenant);
    }
}
