namespace ProductApp.Application.Common.Services;

public interface IRefreshTokenCookieService
{
    string Get(bool throwException = true);
    void Set(string refreshToken);
    void Clear();
}