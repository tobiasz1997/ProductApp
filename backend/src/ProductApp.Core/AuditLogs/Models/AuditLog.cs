using ProductApp.Core.Common.ValueObjects;

namespace ProductApp.Core.AuditLogs.Models;

public class AuditLog(Id id, DateTime createdAt, string action, Guid? userId, object? metadata)
{
    public Id Id { get; private set; } = id;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public string Action { get; private set; } = action;
    public Guid? UserId { get; private set; } = userId;
    public object? Metadata { get; private set; } = metadata;
}