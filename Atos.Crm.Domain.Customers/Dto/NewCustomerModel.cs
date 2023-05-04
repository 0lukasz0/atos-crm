using System.Text.Json.Serialization;

namespace Atos.Crm.Domain.Customers.Dto
{
    public class NewCustomerModel
    {
        [JsonPropertyName("firstName")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastName")]
        public string Surname { get; set; }
    }
}