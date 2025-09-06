using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FrontierCRM.Domain.Entities;
using FrontierCRM.Domain.ValueObjects;

namespace FrontierCRM.Infrastructure.Data;

/// <summary>
/// Service for seeding the database with initial data
/// </summary>
public class DatabaseSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(IServiceProvider serviceProvider, ILogger<DatabaseSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync(cancellationToken);

            // Run migrations
            await context.Database.MigrateAsync(cancellationToken);

            // Seed initial data
            await SeedInitialDataAsync(context, cancellationToken);

            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedInitialDataAsync(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        // Check if data already exists
        if (await context.Tenants.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Database already contains data, skipping seed");
            return;
        }

        _logger.LogInformation("Seeding initial data...");

        // Create default tenant
        var defaultTenant = new Tenant("Default Tenant", "default")
        {
            Website = "https://frontiercrm.com",
            Industry = "Technology",
            Settings = new TenantSettings
            {
                TimeZone = "UTC",
                Locale = "en-US",
                Currency = "USD",
                MaxUsers = 100,
                MaxStorageBytes = 10L * 1024 * 1024 * 1024, // 10GB
                MaxApiCallsPerMonth = 100000,
                AllowCustomFields = true,
                AllowWorkflows = true,
                AllowIntegrations = true
            }
        };

        context.Tenants.Add(defaultTenant);
        await context.SaveChangesAsync(cancellationToken);

        // Create default roles
        var adminRole = new Role(defaultTenant.Id, "Administrator", "Full system access", true);
        var userRole = new Role(defaultTenant.Id, "User", "Standard user access", true);
        var readonlyRole = new Role(defaultTenant.Id, "ReadOnly", "Read-only access", true);

        context.Roles.AddRange(adminRole, userRole, readonlyRole);
        await context.SaveChangesAsync(cancellationToken);

        // Create default admin user
        var adminUser = new User(defaultTenant.Id, "admin@frontiercrm.com", "Admin", "User")
        {
            JobTitle = "System Administrator",
            Department = "IT",
            Preferences = new UserPreferences
            {
                TimeZone = "UTC",
                Locale = "en-US",
                DateFormat = "MM/dd/yyyy",
                TimeFormat = "12h",
                Currency = "USD",
                EmailNotifications = true,
                PushNotifications = true,
                DesktopNotifications = true,
                ItemsPerPage = 25,
                Theme = "light"
            }
        };

        context.Users.Add(adminUser);
        await context.SaveChangesAsync(cancellationToken);

        // Assign admin role to admin user
        var adminUserRole = new UserRole(adminUser.Id, adminRole.Id, adminUser.Id, defaultTenant.Id);
        context.UserRoles.Add(adminUserRole);

        // Add some basic permissions to roles
        var adminPermissions = new[]
        {
            "users.create", "users.read", "users.update", "users.delete",
            "tenants.create", "tenants.read", "tenants.update", "tenants.delete",
            "roles.create", "roles.read", "roles.update", "roles.delete",
            "accounts.create", "accounts.read", "accounts.update", "accounts.delete",
            "contacts.create", "contacts.read", "contacts.update", "contacts.delete",
            "leads.create", "leads.read", "leads.update", "leads.delete",
            "opportunities.create", "opportunities.read", "opportunities.update", "opportunities.delete"
        };

        var userPermissions = new[]
        {
            "users.read", "accounts.read", "accounts.update", "accounts.create",
            "contacts.read", "contacts.update", "contacts.create",
            "leads.read", "leads.update", "leads.create",
            "opportunities.read", "opportunities.update", "opportunities.create"
        };

        var readonlyPermissions = new[]
        {
            "users.read", "accounts.read", "contacts.read", "leads.read", "opportunities.read"
        };

        // Add permissions to admin role
        foreach (var permission in adminPermissions)
        {
            context.RolePermissions.Add(new RolePermission(adminRole.Id, permission, adminUser.Id, defaultTenant.Id));
        }

        // Add permissions to user role
        foreach (var permission in userPermissions)
        {
            context.RolePermissions.Add(new RolePermission(userRole.Id, permission, adminUser.Id, defaultTenant.Id));
        }

        // Add permissions to readonly role
        foreach (var permission in readonlyPermissions)
        {
            context.RolePermissions.Add(new RolePermission(readonlyRole.Id, permission, adminUser.Id, defaultTenant.Id));
        }

        await context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Initial data seeded successfully");
    }
}
