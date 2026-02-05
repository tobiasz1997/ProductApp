using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Core.AuditLogs.Repositories;
using ProductApp.Core.Common.Repositories;
using ProductApp.Core.ProductFavourites.Repositories;
using ProductApp.Core.Users.Repositories;
using ProductApp.Infrastructure.DAL.App;
using ProductApp.Infrastructure.DAL.App.Repositories;
using ProductApp.Infrastructure.DAL.Audit;
using ProductApp.Infrastructure.DAL.Audit.Repositories;
using ProductApp.Infrastructure.DAL.Decorators;

namespace ProductApp.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "database";
    
    public static IServiceCollection AddPostgres(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<PostgresOptions>(configuration.GetRequiredSection(SectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(SectionName);

        service.AddDbContext<AppDatabaseContext>(x => x.UseNpgsql(postgresOptions.ConnectionString, y => y.MigrationsHistoryTable("__EFMigrationHistory", "app")));
        service.AddDbContext<AuditDatabaseContext>(x => x.UseNpgsql(postgresOptions.ConnectionString, y => y.MigrationsHistoryTable("__EFMigrationHistory", "audit")));
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IProductFavouriteRepository, ProductFavouriteRepository>();
        service.AddScoped<IUserFavouriteProductsRepository, UserFavouriteProductsRepository>();
        service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        service.AddScoped<IAuditLogRepository, AuditLogRepository>();
        service.AddScoped<IUnitOfWork, DatabaseUnitOfWork>();
        service.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        service.TryDecorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));
        service.AddHostedService<DatabaseInitializer>();

        return service;
    }
}