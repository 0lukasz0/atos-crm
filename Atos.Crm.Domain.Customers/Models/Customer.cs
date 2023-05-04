using Atos.Crm.Core;

namespace Atos.Crm.Domain.Customers.Models
{
    public class Customer : AggregateRoot
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}