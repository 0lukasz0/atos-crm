using Atos.Crm.Abstractions;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Dto;
using Atos.Crm.Domain.Customers.Models;

namespace Atos.Crm.Domain.Customers.Commands;

public record CreateCustomerCommand(NewCustomerModel NewCustomerModel) : ICommand;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, int>
{
    private readonly IRepository<Customer> _customersRepository;

    public CreateCustomerCommandHandler(IRepository<Customer> customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<int> HandleAsync(CreateCustomerCommand command)
    {
        var model = command.NewCustomerModel;
        var customer = new Customer
        {
            Firstname = model.Firstname,
            Surname = model.Surname
        };

        return await _customersRepository.CreateAsync(customer);
    }
}