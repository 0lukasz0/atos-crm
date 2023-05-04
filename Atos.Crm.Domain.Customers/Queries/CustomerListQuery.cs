using Atos.Crm.Abstractions;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Dto;
using Atos.Crm.Domain.Customers.Models;

namespace Atos.Crm.Domain.Customers.Queries;

public interface ICustomerListQuery : IQuery
{
    IReadOnlyList<CustomerListModel> ListAll();
}

public sealed class CustomerListQuery : ICustomerListQuery
{
    private readonly IRepository<Customer> _customersRepository;

    public CustomerListQuery(IRepository<Customer> customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public IReadOnlyList<CustomerListModel> ListAll()
    {
        var customerListModels = _customersRepository.Query()
            .Select(c => new CustomerListModel(c.Id, $"{c.Firstname} {c.Surname}"))
            .OrderBy(c => c.Id)
            .ToList();

        return customerListModels;
    }
}