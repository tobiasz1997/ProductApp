using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.Repositories;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DatabaseContext _databaseContext;

    public RefreshTokenRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public Task<RefreshToken?> GetByToken(Token token) => _databaseContext.RefreshToken.SingleOrDefaultAsync(x => x.Token == token);

    public Task<RefreshToken?> GetByUserId(Id userId) => _databaseContext.RefreshToken.SingleOrDefaultAsync(x => x.UserId == userId);

    public Task Insert(RefreshToken refreshToken)
    {
        _databaseContext.RefreshToken.AddAsync(refreshToken);
        return Task.CompletedTask;
    }

    public Task Update(RefreshToken token)
    {
        _databaseContext.RefreshToken.Update(token);
        return Task.CompletedTask;
    }

    public Task Delete(RefreshToken token)
    {
        _databaseContext.RefreshToken.Remove(token);
        return Task.CompletedTask;
    }
}