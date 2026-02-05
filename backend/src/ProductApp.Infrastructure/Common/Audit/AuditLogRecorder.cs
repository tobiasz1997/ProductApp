using Microsoft.Extensions.Logging;
using ProductApp.Application.Common.Services;
using ProductApp.Application.Favourites.Commands;
using ProductApp.Application.User.Commands;
using ProductApp.Core.AuditLogs.Repositories;
using ProductApp.Infrastructure.DAL.Audit;
using AuditLogModel = ProductApp.Core.AuditLogs.Models.AuditLog;

namespace ProductApp.Infrastructure.Common.Audit;

public class AuditLogRecorder : IAuditLogRecorder
{
    private readonly IClock _clock;
    private readonly AuditDatabaseContext _auditDatabaseContext;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly ILogger<AuditLogRecorder> _logger;

    public AuditLogRecorder(IClock clock, AuditDatabaseContext auditDatabaseContext, IAuditLogRepository auditLogRepository, ILogger<AuditLogRecorder> logger)
    {
        _clock = clock;
        _auditDatabaseContext = auditDatabaseContext;
        _auditLogRepository = auditLogRepository;
        _logger = logger;
    }
    
    public async Task RecordLog(object command, string? errorMessage = null)
    {
        await using var transaction = await _auditDatabaseContext.Database.BeginTransactionAsync();

        try
        {
            var result = CreateLog(command, errorMessage);
            if (result is not null)
            {
                await _auditLogRepository.AddAsync(result);
                await _auditDatabaseContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }

    private AuditLogModel? CreateLog(object command, string? errorMessage) => command switch
    {
        SignUp x => new AuditLogModel(Guid.NewGuid(), _clock.Current(), nameof(SignUp), null, new { x.Login, errorMessage }),
        AddFavouriteProduct x => new AuditLogModel(Guid.NewGuid(), _clock.Current(), nameof(AddFavouriteProduct), x.UserId,
            new { x.ExternalId, errorMessage }),
        DeleteFavouriteProduct x => new AuditLogModel(Guid.NewGuid(), _clock.Current(), nameof(AddFavouriteProduct), x.UserId,
            new { x.ProductId, errorMessage }),
        _ => null
    };
}