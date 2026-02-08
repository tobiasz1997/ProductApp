using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;
using ProductApp.Infrastructure.Common.AuditLog;

namespace ProductApp.Infrastructure.Common.Audit;

public static class Extension
{
    public static IServiceCollection AddAuditLog(this IServiceCollection service)
    {
        service.AddScoped<IAuditLogRecorder, AuditLogRecorder>();
        service.TryDecorate(typeof(ICommandHandler<>), typeof(AuditLogCommandHandlerDecorator<>));
        service.TryDecorate(typeof(ICommandHandler<,>), typeof(AuditLogCommandHandlerDecorator<,>));

        return service;
    }
}