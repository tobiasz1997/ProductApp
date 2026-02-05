using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductApp.Application.Common.Services;
using ProductApp.Core.Users.Models;

namespace ProductApp.Infrastructure.Common.Auth;

internal static class Extensions
{
    private const string SectionName = "auth"; 
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthOptions>(SectionName);
        
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));

        services
            .AddHttpContextAccessor()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordService, PasswordService>()
            .AddSingleton<IJwtService, JwtService>()
            .AddSingleton<IRefreshTokenCookieService, RefreshTokenCookieService>()
            .AddSingleton<IRefreshTokenService, RefreshTokenService>()
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = options.Audience;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });

        services.AddAuthorization();

        return services;
    }
}