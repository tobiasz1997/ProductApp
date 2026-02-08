namespace ProductApp.Infrastructure.DAL.Decorators;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
    Task<T> ExecuteAsync<T>(Func<Task<T>> action);
}