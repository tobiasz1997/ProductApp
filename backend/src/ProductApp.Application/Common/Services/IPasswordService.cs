namespace ProductApp.Application.Common.Services;

public interface IPasswordService
{
    string Secure(string password);
    bool Validate(string password, string securePassword);
}