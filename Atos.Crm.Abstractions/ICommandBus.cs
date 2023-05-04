namespace Atos.Crm.Abstractions;

public interface ICommandBus
{
    Task<TResult> ExecuteAsync<TResult, TCommand>(TCommand command) 
        where TCommand : ICommand;
}