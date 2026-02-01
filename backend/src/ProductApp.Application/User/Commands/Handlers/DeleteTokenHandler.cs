using ProductApp.Application.Common.Abstraction;
using ProductApp.Core.Users.Repositories;

namespace ProductApp.Application.User.Commands.Handlers;

internal sealed class DeleteTokenHandler : ICommandHandler<DeleteToken>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public DeleteTokenHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task HandleAsync(DeleteToken command)
    {
        var token = await _refreshTokenRepository.GetByToken(command.Token);

        if (token is not null)
        {
            await _refreshTokenRepository.Delete(token);
        }
    }
}