using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Favourites.Commands;
using ProductApp.Application.Favourites.Commands.Handlers;
using ProductApp.Application.Favourites.DTO;
using ProductApp.Application.Favourites.Queries;
using ProductApp.Application.Favourites.Queries.Handlers;
using ProductApp.Application.User.Commands;
using ProductApp.Application.User.Commands.Handlers;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Queries;
using ProductApp.Application.User.Queries.Handlers;

namespace ProductApp.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<ICommandHandler<SignIn, AuthResultDto>, SignInHandler>()
            .AddScoped<ICommandHandler<SignUp, AuthResultDto>, SignUpHandler>()
            .AddScoped<ICommandHandler<RefreshToken, AuthResultDto>, RefreshTokenHandler>()
            .AddScoped<ICommandHandler<DeleteToken>, DeleteTokenHandler>()
            .AddScoped<IQueryHandler<GetUser, UserDto>, GetUserHandler>()
            .AddScoped<ICommandHandler<AddFavouriteProduct, Guid>, AddFavouriteProductHandler>()
            .AddScoped<ICommandHandler<DeleteFavouriteProduct>, DeleteFavouriteProductHandler>()
            .AddScoped<IQueryHandler<GetFavouriteProductsList, IEnumerable<ProductDto>>, GetFavouriteProductsListHandler>();
        
        // TODO: problem with Structor and .NET9, deeper check or update to .net 10 is required.
        // var applicationAssemblyCommands = typeof(ICommandHandler<>).Assembly;
        // var applicationAssemblyQueries = typeof(IQueryHandler<,>).Assembly;
        
        // services.Scan(s =>
        //     s.FromAssemblies(applicationAssemblyCommands)
        //         .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
        //         .AsImplementedInterfaces()
        //         .WithScopedLifetime());
        //
        // services.Scan(s =>
        //     s.FromAssemblies(applicationAssemblyQueries)
        //         .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
        //         .AsImplementedInterfaces()
        //         .WithScopedLifetime());

        return services;
    }
}