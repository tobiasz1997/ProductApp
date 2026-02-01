using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Core.Users.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByToken(Token token);
    Task<RefreshToken?> GetByUserId(Id userId);
    Task Insert(RefreshToken refreshToken);
    Task Update(RefreshToken token);
    Task Delete(RefreshToken token);
}