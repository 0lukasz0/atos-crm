using System.Linq;
using System.Threading.Tasks;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Commands;
using Atos.Crm.Domain.Customers.Dto;
using Atos.Crm.Domain.Customers.Models;
using Atos.Crm.Infrastructure.DataAccess;
using NUnit.Framework;

namespace Atos.Crm.Domain.Customers.Tests
{
    public class CreateCustomerTests
    {
        private CreateCustomerCommandHandler sut;
        private IRepository<Customer> repository;

        [SetUp]
        public void Setup()
        {
            repository = new Repository<Customer>(new DbContext<Customer>());
            sut = new CreateCustomerCommandHandler(repository);
        }

        [Test]
        public async Task AddingCustomer_Works()
        {
            // Arrange
            var model = new NewCustomerModel
            {
                Firstname = "John",
                Surname = "Doe"
            };

            // Act
            await sut.HandleAsync(new CreateCustomerCommand(model));

            // Assert
            var actual = repository.Query().OrderBy(c => c.Id).ToList();
            var first = actual.FirstOrDefault();

            Assert.That(actual.Count, Is.EqualTo(1));
            Assert.That(first, Is.Not.Null);
            Assert.That(first.Id, Is.EqualTo(1));
            Assert.That(first.Firstname, Is.EqualTo(model.Firstname));
            Assert.That(first.Surname, Is.EqualTo(model.Surname));
        }

        [Test]
        public async Task AddingMultipleCustomer_Works()
        {
            // Arrange
            var model = new NewCustomerModel
            {
                Firstname = "John",
                Surname = "Doe"
            };

            var model2 = new NewCustomerModel
            {
                Firstname = "Joe",
                Surname = "Smith"
            };

            // Act
            await sut.HandleAsync(new CreateCustomerCommand(model));
            await sut.HandleAsync(new CreateCustomerCommand(model2));


            // Assert
            var actual = repository.Query().OrderBy(c => c.Id).ToList();
            var first = actual.FirstOrDefault();
            var second = actual.Skip(1).FirstOrDefault();
            
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(first, Is.Not.Null);
            Assert.That(first.Id, Is.EqualTo(1));
            Assert.That(first.Firstname, Is.EqualTo(model.Firstname));
            Assert.That(first.Surname, Is.EqualTo(model.Surname));

            Assert.That(second, Is.Not.Null);
            Assert.That(second.Id, Is.EqualTo(2));
            Assert.That(second.Firstname, Is.EqualTo(model2.Firstname));
            Assert.That(second.Surname, Is.EqualTo(model2.Surname));
        }
    }
}