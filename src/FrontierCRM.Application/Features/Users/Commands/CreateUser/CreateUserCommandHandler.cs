using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FrontierCRM.Application.Common.Interfaces;
using FrontierCRM.Application.Common.DTOs;
using FrontierCRM.Domain.Entities;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Handler for creating a new user
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITenantProvider _tenantProvider;
    private readonly IDateTime _dateTime;

    public CreateUserCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ITenantProvider tenantProvider,
        IDateTime dateTime)
    {
        _context = context;
        _mapper = mapper;
        _tenantProvider = tenantProvider;
        _dateTime = dateTime;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Check if email is already taken in this tenant
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.TenantId == _tenantProvider.TenantId && u.Email == request.Email, cancellationToken);
        
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with email '{request.Email}' already exists in this tenant.");
        }

        // Create user preferences
        var preferences = new UserPreferences
        {
            TimeZone = request.Preferences.TimeZone,
            Locale = request.Preferences.Locale,
            DateFormat = request.Preferences.DateFormat,
            TimeFormat = request.Preferences.TimeFormat,
            Currency = request.Preferences.Currency,
            EmailNotifications = request.Preferences.EmailNotifications,
            PushNotifications = request.Preferences.PushNotifications,
            DesktopNotifications = request.Preferences.DesktopNotifications,
            ItemsPerPage = request.Preferences.ItemsPerPage,
            Theme = request.Preferences.Theme,
            CustomPreferences = request.Preferences.CustomPreferences
        };

        // Create user
        var user = new User(_tenantProvider.TenantId, request.Email, request.FirstName, request.LastName)
        {
            Phone = request.Phone,
            JobTitle = request.JobTitle,
            Department = request.Department,
            Preferences = preferences
        };

        _context.Users.Add(user);

        // Assign roles if provided
        if (request.RoleIds.Any())
        {
            var roles = await _context.Roles
                .Where(r => r.TenantId == _tenantProvider.TenantId && request.RoleIds.Contains(r.Id))
                .ToListAsync(cancellationToken);

            foreach (var role in roles)
            {
                var userRole = new UserRole(user.Id, role.Id);
                _context.UserRoles.Add(userRole);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
