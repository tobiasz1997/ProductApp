using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductApp.Infrastructure.DAL.App;
using ProductApp.Infrastructure.DAL.Audit;

namespace ProductApp.Infrastructure.DAL;

public class DatabaseInitializer: IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
     
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
            
        var auditDbContext = scope.ServiceProvider.GetRequiredService<AuditDatabaseContext>();
        await auditDbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}