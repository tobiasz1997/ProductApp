namespace ProductApp.Infrastructure.DAL.Decorators;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}