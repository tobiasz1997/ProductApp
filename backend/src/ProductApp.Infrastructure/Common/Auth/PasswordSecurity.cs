using Microsoft.AspNetCore.Identity;
using ProductApp.Application.Common.Services;
using ProductApp.Core.Users.Models;

namespace ProductApp.Infrastructure.Common.Auth;

internal sealed class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securePassword) =>
        _passwordHasher.VerifyHashedPassword(default, securePassword, password) is PasswordVerificationResult.Success;
}