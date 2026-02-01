namespace ProductApp.Application.Common.Abstraction;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}