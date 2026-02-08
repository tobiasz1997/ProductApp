namespace ProductApp.Application.Common.Services;

public interface IAuditLogRecorder
{
    Task RecordLog(object command, string? errorMessage = null);
}