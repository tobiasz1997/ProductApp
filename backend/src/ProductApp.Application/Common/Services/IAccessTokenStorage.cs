using ProductApp.Application.User.DTO;

namespace ProductApp.Application.Common.Services;

public interface IAccessTokenStorage
{
    void Set(AuthResultDto jwt);
    AuthResultDto Get();
}