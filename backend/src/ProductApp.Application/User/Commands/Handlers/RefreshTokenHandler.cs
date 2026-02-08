using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Exceptions;
using ProductApp.Core.Users.Repositories;

namespace ProductApp.Application.User.Commands.Handlers;

internal sealed class RefreshTokenHandler : ICommandHandler<RefreshToken, AuthResultDto>
{
    private readonly IClock _clock;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokenHandler(
        IRefreshTokenService refreshTokenService, 
        IRefreshTokenRepository refreshTokenRepository, 
        IClock clock,
        IJwtService jwtService, 
        IUserRepository userRepository)
    {
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _clock = clock;
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    public async Task<AuthResultDto> HandleAsync(RefreshToken command)
    {
        var token = await _refreshTokenRepository.GetByToken(command.Token);

        if (token is null || token.ExpiresAt < _clock.Current())
        {
            throw new AuthenticationException();
        }

        var user = await _userRepository.GetByIdAsync(token.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(token.UserId);
        }

        var accessToken = _jwtService.CreateToken(user.Id, user.Login);
        token = _refreshTokenService.Refresh(token);
        await _refreshTokenRepository.Update(token);

        return new AuthResultDto { AccessToken = accessToken, RefreshToken = token.Token };
    }
}