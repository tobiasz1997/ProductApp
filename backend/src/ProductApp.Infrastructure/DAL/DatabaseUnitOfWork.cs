using ProductApp.Infrastructure.DAL.App;
using ProductApp.Infrastructure.DAL.Decorators;

namespace ProductApp.Infrastructure.DAL;

public class DatabaseUnitOfWork: IUnitOfWork
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public DatabaseUnitOfWork(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext;
    }
    
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _appDatabaseContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _appDatabaseContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
    {
        await using var transaction = await _appDatabaseContext.Database.BeginTransactionAsync();

        try
        {
            var result = await action();
            await _appDatabaseContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}