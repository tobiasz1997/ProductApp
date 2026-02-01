using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Core.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Id id);
    Task<User?> GetByLoginAsync(Login login);
    Task AddAsync(User user);
}