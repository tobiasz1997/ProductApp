using ProductApp.Core.AuditLogs.Models;

namespace ProductApp.Core.AuditLogs.Repositories;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog product);
}