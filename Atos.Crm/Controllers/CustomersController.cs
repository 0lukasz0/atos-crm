using Atos.Crm.Abstractions;
using Atos.Crm.Domain.Customers.Commands;
using Atos.Crm.Domain.Customers.Dto;
using Atos.Crm.Domain.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Atos.Crm.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerListQuery _customerListQuery;
        private readonly ICommandBus _commandBus;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICustomerListQuery customerListQuery,
            ICommandBus commandBus,
            ILogger<CustomersController> logger)
        {
            _customerListQuery = customerListQuery;
            _commandBus = commandBus;
            _logger = logger;
        }

        [HttpPost]
        public async Task<int> Create([FromBody] NewCustomerModel model)
        {
            var command = new CreateCustomerCommand(model);
            return await _commandBus.ExecuteAsync<int, CreateCustomerCommand>(command);
        }

        [HttpGet]
        public IEnumerable<CustomerListModel> ListAll()
        {
            return _customerListQuery.ListAll();
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            var command = new DeleteCustomerCommand(id);
            return await _commandBus.ExecuteAsync<bool, DeleteCustomerCommand>(command);
        }
    }
}