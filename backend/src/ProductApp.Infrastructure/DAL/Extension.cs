using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Core.Favourites.Repositories;
using ProductApp.Core.Users.Repositories;
using ProductApp.Infrastructure.DAL.Decorators;
using ProductApp.Infrastructure.DAL.Repositories;

namespace ProductApp.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "database";
    
    public static IServiceCollection AddPostgres(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<PostgresOptions>(configuration.GetRequiredSection(SectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(SectionName);

        service.AddDbContext<DatabaseContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        service.AddScoped<IProductFavouriteRepository, ProductFavouriteRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        service.AddScoped<IUnitOfWork, DatabaseUnitOfWork>();
        service.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        service.AddHostedService<DatabaseInitializer>();

        return service;
    }
}