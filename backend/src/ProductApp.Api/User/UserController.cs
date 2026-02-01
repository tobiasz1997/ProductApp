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
[Route("user")]
public class UserController : BaseApiController
{
    private readonly ICommandHandler<SignInOrCreate> _signInOrCreateCommandHandler;
    private readonly ICommandHandler<RefreshToken> _refreshTokenCommandHandler;
    private readonly ICommandHandler<DeleteToken> _deleteTokenCommandHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserQueryHandler;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;

    public UserController(
        ICommandHandler<SignInOrCreate> signInOrCreateCommandHandler, 
        ICommandHandler<RefreshToken> refreshTokenCommandHandler,
        IQueryHandler<GetUser, UserDto> getUserQueryHandler, 
        IAccessTokenStorage tokenStorage, IRefreshTokenCookieService refreshTokenCookieService, 
        ICommandHandler<DeleteToken> deleteTokenCommandHandler)
    {
        _signInOrCreateCommandHandler = signInOrCreateCommandHandler;
        _refreshTokenCommandHandler = refreshTokenCommandHandler;
        _getUserQueryHandler = getUserQueryHandler;
        _tokenStorage = tokenStorage;
        _refreshTokenCookieService = refreshTokenCookieService;
        _deleteTokenCommandHandler = deleteTokenCommandHandler;
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        return Ok(await _getUserQueryHandler.HandleAsync(new GetUser {UserId = UserId}));
    }
    
    [HttpPost("token/refresh")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var refreshToken = _refreshTokenCookieService.Get();
        await _refreshTokenCommandHandler.HandleAsync(new RefreshToken(refreshToken));
        var result = _tokenStorage.Get();
        _refreshTokenCookieService.Set(result.RefreshToken);
        return Ok(result.AccessToken);
    }
    
    [HttpPost("sign-in-or-create")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> SignInOrCreate([FromBody] SignInRequest command)
    {
        await _signInOrCreateCommandHandler.HandleAsync(new SignInOrCreate(command.Login, command.Password));
        var result = _tokenStorage.Get();
        _refreshTokenCookieService.Set(result.RefreshToken);
        return Ok(result.AccessToken);
    }
    
    [HttpPost("logout")]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Logout()
    {
        var refreshToken = _refreshTokenCookieService.Get(false);
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _deleteTokenCommandHandler.HandleAsync(new DeleteToken(refreshToken));   
        }
        _refreshTokenCookieService.Clear();
        return NoContent();
    }
}