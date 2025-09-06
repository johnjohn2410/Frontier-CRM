using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FrontierCRM.Application.Common.Interfaces;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Users.Queries.GetUsers;

/// <summary>
/// Handler for getting a list of users
/// </summary>
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITenantProvider _tenantProvider;

    public GetUsersQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ITenantProvider tenantProvider)
    {
        _context = context;
        _mapper = mapper;
        _tenantProvider = tenantProvider;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users
            .Where(u => u.TenantId == _tenantProvider.TenantId)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(u => 
                u.FirstName.ToLower().Contains(searchTerm) ||
                u.LastName.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm) ||
                u.DisplayName.ToLower().Contains(searchTerm));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(u => u.IsActive == request.IsActive.Value);
        }

        // Apply pagination
        var users = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
