using Microsoft.AspNetCore.Mvc;
using MediatR;
using FrontierCRM.Application.Features.Users.Commands.CreateUser;
using FrontierCRM.Application.Features.Users.Queries.GetUsers;
using FrontierCRM.Application.Common.DTOs;

namespace FrontierCRM.Api.Controllers;

/// <summary>
/// Controller for user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="command">The create user command</param>
    /// <returns>The created user ID</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUsers), new { }, new { id = userId });
    }

    /// <summary>
    /// Gets a list of users
    /// </summary>
    /// <param name="query">The get users query</param>
    /// <returns>A list of users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
    {
        var users = await _mediator.Send(query);
        return Ok(users);
    }
}
