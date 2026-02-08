using ProductApp.Core.AuditLogs.Models;
using ProductApp.Core.AuditLogs.Repositories;

namespace ProductApp.Infrastructure.DAL.Audit.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly AuditDatabaseContext _auditDatabaseContext;

    public AuditLogRepository(AuditDatabaseContext auditDatabaseContext)
    {
        _auditDatabaseContext = auditDatabaseContext;
    }

    public Task AddAsync(AuditLog product)
    {
        _auditDatabaseContext.AuditLog.AddAsync(product);
        return Task.CompletedTask;
    }
}