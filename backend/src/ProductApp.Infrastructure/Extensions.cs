using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Common.Services;
using ProductApp.Infrastructure.Common.Audit;
using ProductApp.Infrastructure.Common.Auth;
using ProductApp.Infrastructure.Common.Cors;
using ProductApp.Infrastructure.Common.Middleware;
using ProductApp.Infrastructure.Common.Time;
using ProductApp.Infrastructure.DAL;

namespace ProductApp.Infrastructure;

public static class Extensions
{
    private const string PolicyName = "client"; 
    private const string SectionName = "cors"; 
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration.GetOptions<CorsOptions>(SectionName);
        
        services.AddCors(options =>
        {
            options.AddPolicy("AngularPolicy", p => p
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        });
        
        services
            .AddSingleton<LoggingMiddleware>()
            .AddSingleton<ExceptionMiddleware>()
            .AddPostgres(configuration)
            .AddAuditLog()
            .AddSingleton<IClock, Clock>()
            .AddAuth(configuration)
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseCors("AngularPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}