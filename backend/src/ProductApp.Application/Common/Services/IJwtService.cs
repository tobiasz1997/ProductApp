namespace ProductApp.Application.Common.Services;

public interface IJwtService
{
    string CreateToken(Guid userId, string login);
}