using MediatR;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Application.Features.Users.Queries.GetUsers;

/// <summary>
/// Query to get a list of users
/// </summary>
public record GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 25;
    public string? SearchTerm { get; init; }
    public bool? IsActive { get; init; }
}
