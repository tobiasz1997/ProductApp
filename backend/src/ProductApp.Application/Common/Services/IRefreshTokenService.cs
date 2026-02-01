using ProductApp.Core.Common.ValueObjects;
using ProductApp.Core.Users.Models;

namespace ProductApp.Application.Common.Services;

public interface IRefreshTokenService
{
    RefreshToken Create(Id userId);
    RefreshToken Refresh(RefreshToken token);
}