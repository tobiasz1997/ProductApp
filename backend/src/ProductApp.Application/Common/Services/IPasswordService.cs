using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Application.Common.Services;

public interface IPasswordService
{
    string Secure(Password password);
    bool Validate(string password, string securePassword);
}