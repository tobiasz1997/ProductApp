using ProductApp.Infrastructure.DAL.Decorators;

namespace ProductApp.Infrastructure.DAL;

public class DatabaseUnitOfWork: IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    public DatabaseUnitOfWork(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}