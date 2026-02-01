using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Core.Users.Models;

public class RefreshToken(Id id, Id userId, Token token, DateTime expiresAt)
{
    public Id Id { get; private set; } = id;
    public Id UserId { get; private set; } = userId;
    public Token Token { get; private set; } = token;
    public DateTime ExpiresAt { get; private set; } = expiresAt;

    public void UpdateExpiryDate(DateTime dateTime)
    {
        ExpiresAt = dateTime;
    }
}