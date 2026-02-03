using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.Common;
using ProductApp.Api.User.Requests;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.Commands;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Queries;

namespace ProductApp.Api.User;

[ApiController]
[Authorize]
[Route("user")]
public class UserController : BaseApiController
{
    private readonly IQueryHandler<GetUser, UserDto> _getUserQueryHandler;

    public UserController(IQueryHandler<GetUser, UserDto> getUserQueryHandler)
    {
        _getUserQueryHandler = getUserQueryHandler;
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        return Ok(await _getUserQueryHandler.HandleAsync(new GetUser {UserId = UserId}));
    }
}