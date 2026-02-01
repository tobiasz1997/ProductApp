using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProductApp.Application.Common.Services;
using ProductApp.Infrastructure.Common.Exception;

namespace ProductApp.Infrastructure.Common.Auth;

public sealed class RefreshTokenCookieService : IRefreshTokenCookieService
{
    private readonly IClock _clock;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly int _refreshTokenExpiry;
    
    private const string RefreshTokenKey = "refreshToken";
    private const string Path = "auth/refresh";

    public RefreshTokenCookieService(IHttpContextAccessor httpContextAccessor, IClock clock, IOptions<AuthOptions> options)
    {
        _httpContextAccessor = httpContextAccessor;
        _clock = clock;
        _refreshTokenExpiry = options.Value.RefreshTokenExpiryInDays;
    }

    public string Get(bool throwException = true)
    {
        var refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(RefreshTokenKey, out var rt);
        if (!refreshToken && throwException)
        {
            throw new AuthorizationException();
        }

        return rt ?? string.Empty;
    }

    public void Set(string refreshToken)
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Append(RefreshTokenKey, refreshToken, new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = Path,
            Expires = _clock.Current().AddDays(_refreshTokenExpiry)
        });
    }

    public void Clear()
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Delete(
            RefreshTokenKey,
            new CookieOptions()
            {
                Path = Path,
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true
            }
        );
    }
}