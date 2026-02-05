using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.Common.Services;

namespace ProductApp.Infrastructure.Common.AuditLog;

internal sealed class AuditLogCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IAuditLogRecorder _auditLogRecorder;

    public AuditLogCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IAuditLogRecorder auditLogRecorder)
    {
        _commandHandler = commandHandler;
        _auditLogRecorder = auditLogRecorder;
    }

    public async Task HandleAsync(TCommand command)
    {
        try
        {
            await _commandHandler.HandleAsync(command);
            await _auditLogRecorder.RecordLog(command);
        }
        catch  (System.Exception ex)
        {
            await _auditLogRecorder.RecordLog(command, ex.Message);
            throw;
        }
    }
}

internal sealed class AuditLogCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _commandHandler;
    private readonly IAuditLogRecorder _auditLogRecorder;

    public AuditLogCommandHandlerDecorator(ICommandHandler<TCommand, TResult> commandHandler, IAuditLogRecorder auditLogRecorder)
    {
        _commandHandler = commandHandler;
        _auditLogRecorder = auditLogRecorder;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        try
        {
            var result = await _commandHandler.HandleAsync(command);
            await _auditLogRecorder.RecordLog(command);
            return result;
        }
        catch  (System.Exception ex)
        {
            await _auditLogRecorder.RecordLog(command, ex.Message);
            throw;
        }
    }
}