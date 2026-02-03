using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Exceptions;
using ProductApp.Core.Users.Repositories;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Application.User.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IClock _clock;
    private readonly IPasswordService _passwordService;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public SignUpHandler(
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

    public async Task HandleAsync(SignUp command)
    {
        if (await _userRepository.GetByLoginAsync(command.Login) is not null)
        {
            throw new LoginInUseException();
        }
        
        var passwordHash = _passwordService.Secure(new Password(command.Password));
        var user = new Core.Users.Models.User(Guid.NewGuid(), command.Login, passwordHash, _clock.Current());

        await _userRepository.AddAsync(user);
        
        var accessToken = _jwtService.CreateToken(user.Id, user.Login);
        var refreshToken = _refreshTokenService.Create(user.Id);

        await _refreshTokenRepository.Insert(refreshToken);
        
        _tokenStorage.Set(new AuthResultDto() { AccessToken = accessToken, RefreshToken = refreshToken.Token});
    }
}