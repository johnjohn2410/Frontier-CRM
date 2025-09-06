using Microsoft.EntityFrameworkCore;
using FrontierCRM.Domain.Entities;

namespace FrontierCRM.Application.Common.Interfaces;

/// <summary>
/// Application database context interface
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<RolePermission> RolePermissions { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
