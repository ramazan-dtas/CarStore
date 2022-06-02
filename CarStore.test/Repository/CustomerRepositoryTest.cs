using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Helpers;
using CarStore.Repository.CustomerRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class CustomerRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly CustomerRepository _customerRepository;

        public CustomerRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreCustomer")
                .Options;

            _context = new(_options);
            _customerRepository = new(_context);


        }

        [Fact]
        public async void SelectAllCustomer_ShouldReturnListOfCustomers_WhenCustomersExists()
        {// Arrange
            await _context.Database.EnsureDeletedAsync();


            // Tilføj Users
            _context.User.Add(
            new User
            {
                Id = 1,
                Email = "ramazan@admins.com",
                Password = "Passw0rd",
                Role = Role.Admin
            });

            _context.User.Add(
            new User
            {
                Id = 2,
                Email = "ramazan@users.com",
                Password = "Passw0rd",
                Role = Role.User
            });

            _context.Customer.Add(
            new Customer
            {
                Id = 1,
                AddressName = "TEC Ballerup",
                ZipCode = 2750,
                CityName = "Ballerup",
                UserId = 1,
            });

            _context.Customer.Add(
            new Customer
            {
                Id = 2,
                AddressName = "Test",
                ZipCode = 1234,
                CityName = "Et sted",
                UserId = 2
            });


            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.SelectAllCustomer();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllCustomers_ShouldReturnEmptyListOfCustomers_WhenNoCustomersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _customerRepository.SelectAllCustomer();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectCustomerById_ShouldReturnCustomer_WhenCustomerExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            _context.Customer.Add(new()
            {
                Id = customerId,
                AddressName = "Telegravej 8",
                ZipCode = 2750,
                CityName = "Ballerup"
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _customerRepository.SelectCustomerById(customerId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async void SelectCustomerById_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _customerRepository.SelectCustomerById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewCustomer_ShouldAddNewIdToCustomer_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Customer customer = new()
            {
                Id = expectedNewId,
                AddressName = "Telegravej 8",
                ZipCode = 2750,
                CityName = "Ballerup"
            };

            //Act
            var result = await _customerRepository.InsertNewCustomer(customer);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewCustomer_ShouldFailToAddNewCustomer_WhenCustomerIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            Customer customer = new()
            {
                AddressName = "Telegravej 8",
                ZipCode = 2750,
                CityName = "Ballerup"
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _customerRepository.InsertNewCustomer(customer);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnCustomer_WhenCustomerExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int updateId = 1;

            Customer customer = new Customer
            {
                Id = updateId,
                UserId = 1,
                AddressName = "Tec Ballerup",
                ZipCode = 2750
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            Customer updateCustomer = new Customer
            {
                Id = 1,
                UserId = 2,
                AddressName = "Ramazan",
                ZipCode = 3000
            };

            var result = await _customerRepository.UpdateExistingCustomer(updateId, updateCustomer);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(updateId, result.Id);
            Assert.Equal(updateCustomer.AddressName, result.AddressName);
            Assert.Equal(updateCustomer.ZipCode, result.ZipCode);
        }

        [Fact]
        public async void UpdateExistingCustomer_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            Customer updateCustomer = new()
            {
                Id = customerId,
                AddressName = "telegrafvej 9",
                ZipCode = 2750,
                CityName = "ballerup"
            };

            //Act
            var result = await _customerRepository.UpdateExistingCustomer(customerId, updateCustomer);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletedCustomerById_ShouldReturnDeletedCustomer_WhenCustomerIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int customerId = 1;
            Customer customer = new()
            {
                Id = 1,
                // UserId = 1,
                AddressName = "TEC Ballerup",
                ZipCode = 2750
            };
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();


            // Act
            var result = await _customerRepository.DeleteCustomer(customerId);

            var deletedItem = await _customerRepository.SelectAllCustomer();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Empty(deletedItem);
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int customerId = 1;

            // Act
            var result = await _customerRepository.DeleteCustomer(customerId);

            // Assert
            Assert.Null(result);
        }
    }
}
