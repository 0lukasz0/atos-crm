using Atos.Crm.Abstractions;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Models;

namespace Atos.Crm.Domain.Customers.Commands;

public record DeleteCustomerCommand(int Id) : ICommand;

public sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, bool>
{
    private readonly IRepository<Customer> _customersRepository;

    public DeleteCustomerCommandHandler(IRepository<Customer> customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<bool> HandleAsync(DeleteCustomerCommand command)
    {
        return await _customersRepository.DeleteAsync(command.Id);
    }
}