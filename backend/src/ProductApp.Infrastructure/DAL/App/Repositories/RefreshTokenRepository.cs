using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.Repositories;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.App.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public RefreshTokenRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }

    public Task<RefreshToken?> GetByToken(Token token) => _appDatabaseContext.RefreshToken.SingleOrDefaultAsync(x => x.Token == token);

    public Task<RefreshToken?> GetByUserId(Id userId) => _appDatabaseContext.RefreshToken.SingleOrDefaultAsync(x => x.UserId == userId);

    public Task Insert(RefreshToken refreshToken)
    {
        _appDatabaseContext.RefreshToken.AddAsync(refreshToken);
        return Task.CompletedTask;
    }

    public Task Update(RefreshToken token)
    {
        _appDatabaseContext.RefreshToken.Update(token);
        return Task.CompletedTask;
    }

    public Task Delete(RefreshToken token)
    {
        _appDatabaseContext.RefreshToken.Remove(token);
        return Task.CompletedTask;
    }
}