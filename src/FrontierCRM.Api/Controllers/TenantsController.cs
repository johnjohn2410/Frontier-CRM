using Microsoft.AspNetCore.Mvc;
using MediatR;
using FrontierCRM.Application.Features.Tenants.Commands.CreateTenant;
using FrontierCRM.Application.Features.Tenants.Queries.GetTenant;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Api.Controllers;

/// <summary>
/// Controller for tenant operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new tenant
    /// </summary>
    /// <param name="command">The create tenant command</param>
    /// <returns>The created tenant ID</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTenant(CreateTenantCommand command)
    {
        var tenantId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTenant), new { id = tenantId }, new { id = tenantId });
    }

    /// <summary>
    /// Gets a tenant by ID
    /// </summary>
    /// <param name="id">The tenant ID</param>
    /// <returns>The tenant details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TenantDto>> GetTenant(Guid id)
    {
        var tenant = await _mediator.Send(new GetTenantQuery(id));
        
        if (tenant == null)
        {
            return NotFound();
        }

        return Ok(tenant);
    }
}
