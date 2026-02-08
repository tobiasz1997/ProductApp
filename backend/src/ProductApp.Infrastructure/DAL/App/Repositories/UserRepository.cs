using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.Repositories;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.App.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public UserRepository(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }

    public Task<User?> GetByIdAsync(Id id) => _appDatabaseContext.User.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByLoginAsync(Login login) => _appDatabaseContext.User.SingleOrDefaultAsync(x => x.Login == login);

    public Task AddAsync(User user)
    {
        _appDatabaseContext.User.AddAsync(user);
        return Task.CompletedTask;
    }
}