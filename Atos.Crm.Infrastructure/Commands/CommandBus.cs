using Atos.Crm.Abstractions;

namespace Atos.Crm.Infrastructure.Commands;

public class CommandBus : ICommandBus
{
    private readonly IServiceProvider _serviceProvider;

    public CommandBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> ExecuteAsync<TResult, TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>));
        var commandHandler = handler as ICommandHandler<TCommand, TResult>;

        if (commandHandler == null)
            throw new Exception("Command handler not registered.");

        return commandHandler.HandleAsync(command);
    }
}