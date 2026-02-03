using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Exceptions;
using ProductApp.Core.Users.Repositories;

namespace ProductApp.Application.User.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public SignInHandler(
        IUserRepository userRepository,
        IPasswordService passwordService, 
        IAccessTokenStorage tokenStorage, 
        IRefreshTokenService refreshTokenService, 
        IRefreshTokenRepository refreshTokenRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenStorage = tokenStorage;
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
    }

    public async Task HandleAsync(SignIn command)
    {
        var user = await _userRepository.GetByLoginAsync(command.Login);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
   
        if (!_passwordService.Validate(command.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        await RefreshOrCreateUserToken(user);
    }

    private async Task RefreshOrCreateUserToken(Core.Users.Models.User user)
    {
        var accessToken = _jwtService.CreateToken(user.Id, user.Login);
        var token = await _refreshTokenRepository.GetByUserId(user.Id);

        if (token is null)
        {
            token = _refreshTokenService.Create(user.Id);
            await _refreshTokenRepository.Insert(token);
        }
        else
        {
            token = _refreshTokenService.Refresh(token);
            await _refreshTokenRepository.Update(token);
        }

        _tokenStorage.Set(new AuthResultDto() {AccessToken = accessToken, RefreshToken = token.Token});   
    }
}