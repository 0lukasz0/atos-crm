using System.Linq;
using System.Threading.Tasks;
using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Domain.Customers.Commands;
using Atos.Crm.Domain.Customers.Models;
using Atos.Crm.Infrastructure.DataAccess;
using NUnit.Framework;

namespace Atos.Crm.Domain.Customers.Tests
{
    public class DeleteCustomerTests
    {
        private DeleteCustomerCommandHandler sut;
        private IRepository<Customer> repository;

        [SetUp]
        public void Setup()
        {
            repository = new Repository<Customer>(new DbContext<Customer>());
            sut = new DeleteCustomerCommandHandler(repository);
        }

        [TearDown]
        public void TearDown()
        {
            repository = null;
            sut = null;
        }

        [Test]
        public async Task DeletingCustomer_EmptyDb_Works()
        {
            // Act
            var result = await sut.HandleAsync(new DeleteCustomerCommand(1));
            
            // Assert
            var actual = repository.Query().OrderBy(c => c.Id).ToList();
            Assert.That(actual.Count, Is.EqualTo(0));
            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task DeletingCustomer_Finding_Works(int toDelete, int saved)
        {
            // Arrange
            var model = new Customer
            {
                Firstname = "John", Surname = "Doe"
            };

            var model2 = new Customer
            {
                Firstname = "Joe", Surname = "Smith"
            };

            await repository.CreateAsync(model);
            await repository.CreateAsync(model2);

            // Act
            var result = await sut.HandleAsync(new DeleteCustomerCommand(toDelete));

            // Assert
            var actual = repository.Query().OrderBy(c => c.Id).ToList();
            var first = actual.FirstOrDefault();

            Assert.That(first, Is.Not.Null);
            Assert.That(first.Id, Is.EqualTo(saved));
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public async Task DeletingCustomer_NotFinding_Works()
        {
            // Arrange
            var model = new Customer
            {
                Firstname = "John",
                Surname = "Doe"
            };

            var model2 = new Customer
            {
                Firstname = "Joe",
                Surname = "Smith"
            };

            await repository.CreateAsync(model);
            await repository.CreateAsync(model2);

            // Act
            var result = await sut.HandleAsync(new DeleteCustomerCommand(3));

            // Assert
            var actual = repository.Query().OrderBy(c => c.Id).ToList();
            var first = actual.FirstOrDefault();
            var second = actual.Skip(1).FirstOrDefault();

            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(first, Is.Not.Null);
            Assert.That(second, Is.Not.Null);
            Assert.That(first.Id, Is.EqualTo(1));
            Assert.That(second.Id, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(false));
        }
    }
}