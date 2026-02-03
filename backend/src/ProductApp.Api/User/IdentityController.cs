using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.User.Requests;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.Commands;

namespace ProductApp.Api.User;

[ApiController]
[Route("identity")]
public class IdentityController : ControllerBase
{
    private readonly ICommandHandler<SignIn> _signInCommandHandler;
    private readonly ICommandHandler<SignUp> _signUpCommandHandler;
    private readonly ICommandHandler<RefreshToken> _refreshTokenCommandHandler;
    private readonly ICommandHandler<DeleteToken> _deleteTokenCommandHandler;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;

    public IdentityController(
        ICommandHandler<RefreshToken> refreshTokenCommandHandler,
        IAccessTokenStorage tokenStorage, IRefreshTokenCookieService refreshTokenCookieService, 
        ICommandHandler<DeleteToken> deleteTokenCommandHandler, 
        ICommandHandler<SignIn> signInCommandHandler, 
        ICommandHandler<SignUp> signUpCommandHandler)
    {
        _refreshTokenCommandHandler = refreshTokenCommandHandler;
        _tokenStorage = tokenStorage;
        _refreshTokenCookieService = refreshTokenCookieService;
        _deleteTokenCommandHandler = deleteTokenCommandHandler;
        _signInCommandHandler = signInCommandHandler;
        _signUpCommandHandler = signUpCommandHandler;
    }
    
    [HttpPost("token/refresh")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var refreshToken = _refreshTokenCookieService.Get();
        await _refreshTokenCommandHandler.HandleAsync(new RefreshToken(refreshToken));
        var result = _tokenStorage.Get();
        _refreshTokenCookieService.Set(result.RefreshToken);
        return Ok(result.AccessToken);
    }
    
    [HttpPost("logout")]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
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
    
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> SignIn([FromBody] SignInRequest command)
    {
        await _signInCommandHandler.HandleAsync(new SignIn(command.Login, command.Password));
        var result = _tokenStorage.Get();
        _refreshTokenCookieService.Set(result.RefreshToken);
        return Ok(result.AccessToken);
    }
    
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<string>> SignUp([FromBody] SignUpRequest command)
    {
        await _signUpCommandHandler.HandleAsync(new SignUp(command.Login, command.Password));
        var result = _tokenStorage.Get();
        _refreshTokenCookieService.Set(result.RefreshToken);
        return Ok(result.AccessToken);
    }
}