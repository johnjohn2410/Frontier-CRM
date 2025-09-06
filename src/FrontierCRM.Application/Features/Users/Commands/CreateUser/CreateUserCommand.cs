using MediatR;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Command to create a new user
/// </summary>
public record CreateUserCommand : IRequest<Guid>
{
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string? JobTitle { get; init; }
    public string? Department { get; init; }
    public UserPreferencesDto Preferences { get; init; } = new();
    public IList<Guid> RoleIds { get; init; } = new List<Guid>();
}
