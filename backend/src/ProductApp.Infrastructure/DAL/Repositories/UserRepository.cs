using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.Repositories;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task<User?> GetByIdAsync(Id id) => _databaseContext.User.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByLoginAsync(Login login) => _databaseContext.User.SingleOrDefaultAsync(x => x.Login == login);

    public Task AddAsync(User user)
    {
        _databaseContext.User.AddAsync(user);
        return Task.CompletedTask;
    }
}