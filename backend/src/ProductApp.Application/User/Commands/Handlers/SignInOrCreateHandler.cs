using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Exceptions;
using ProductApp.Core.Users.Repositories;

namespace ProductApp.Application.User.Commands.Handlers;

internal sealed class SignInOrCreateHandler : ICommandHandler<SignInOrCreate>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IClock _clock;
    private readonly IPasswordService _passwordService;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public SignInOrCreateHandler(
        IUserRepository userRepository,
        IClock clock,
        IPasswordService passwordService, 
        IAccessTokenStorage tokenStorage, 
        IRefreshTokenService refreshTokenService, 
        IRefreshTokenRepository refreshTokenRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _clock = clock;
        _passwordService = passwordService;
        _tokenStorage = tokenStorage;
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
    }

    public async Task HandleAsync(SignInOrCreate command)
    {
        var user = await _userRepository.GetByLoginAsync(command.Login);
        if (user is null)
        {
            await CreateUser(command);
        }
        else
        {
            if (!_passwordService.Validate(command.Password, user.Password))
            {
                throw new InvalidCredentialsException();
            }

            await RefreshOrCreateUserToken(user);
        }
    }

    private async Task CreateUser(SignInOrCreate command)
    {
        var securedPassword = _passwordService.Secure(command.Password);
        var user = new Core.Users.Models.User(Guid.NewGuid(), command.Login, securedPassword, _clock.Current());

        await _userRepository.AddAsync(user);
        
        var accessToken = _jwtService.CreateToken(user.Id, user.Login);
        var refreshToken = _refreshTokenService.Create(user.Id);

        await _refreshTokenRepository.Insert(refreshToken);
        
        _tokenStorage.Set(new AuthResultDto() { AccessToken = accessToken, RefreshToken = refreshToken.Token});
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