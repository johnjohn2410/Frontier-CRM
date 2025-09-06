using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FrontierCRM.Application.Common.Interfaces;
using FrontierCRM.Application.Common.DTOs;
using FrontierCRM.Domain.Entities;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Application.Features.Tenants.Commands.CreateTenant;

/// <summary>
/// Handler for creating a new tenant
/// </summary>
public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IDateTime _dateTime;

    public CreateTenantCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IDateTime dateTime)
    {
        _context = context;
        _mapper = mapper;
        _dateTime = dateTime;
    }

    public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        // Check if subdomain is already taken
        if (!string.IsNullOrEmpty(request.Subdomain))
        {
            var existingTenant = await _context.Tenants
                .FirstOrDefaultAsync(t => t.Subdomain == request.Subdomain, cancellationToken);
            
            if (existingTenant != null)
            {
                throw new InvalidOperationException($"Subdomain '{request.Subdomain}' is already taken.");
            }
        }

        // Create tenant settings
        var settings = new TenantSettings
        {
            TimeZone = request.Settings.TimeZone,
            Locale = request.Settings.Locale,
            Currency = request.Settings.Currency,
            MaxUsers = request.Settings.MaxUsers,
            MaxStorageBytes = request.Settings.MaxStorageBytes,
            MaxApiCallsPerMonth = request.Settings.MaxApiCallsPerMonth,
            AllowCustomFields = request.Settings.AllowCustomFields,
            AllowWorkflows = request.Settings.AllowWorkflows,
            AllowIntegrations = request.Settings.AllowIntegrations,
            CustomSettings = request.Settings.CustomSettings
        };

        // Create tenant
        var tenant = new Tenant(request.Name, request.Subdomain)
        {
            Website = request.Website,
            Industry = request.Industry,
            Settings = settings
        };

        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync(cancellationToken);

        return tenant.Id;
    }
}
