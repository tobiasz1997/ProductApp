using Microsoft.AspNetCore.Identity;
using ProductApp.Application.Common.Services;
using ProductApp.Core.Users.Models;
using ProductApp.Core.Users.ValueObjects;

namespace ProductApp.Infrastructure.Common.Auth;

internal sealed class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(Password password) => _passwordHasher.HashPassword(default, password.Value);

    public bool Validate(string password, string securePassword) =>
        _passwordHasher.VerifyHashedPassword(default, securePassword, password) is PasswordVerificationResult.Success;
}