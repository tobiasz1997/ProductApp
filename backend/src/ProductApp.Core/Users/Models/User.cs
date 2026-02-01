using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Core.Users.Models;

public class User(Id id, Login login, Password password, DateTime createdAt)
{
    public Id Id { get; private set; } = id;
    public Login Login { get; private set; } = login;
    public Password Password { get; private set; } = password;
    public DateTime CreatedAt { get; private set; } = createdAt;
}