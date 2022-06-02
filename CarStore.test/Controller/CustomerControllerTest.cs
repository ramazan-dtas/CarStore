using CarStore.Controllers;
using CarStore.DTO.Customer.Request;
using CarStore.DTO.Customer.Response;
using CarStore.Services.CustomerService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Controller
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _customerController;
        private readonly Mock<ICustomerService> _mockcustomerService = new();

        public CustomerControllerTest()
        {
            _customerController = new(_mockcustomerService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCustomersExists()
        {

            //arrange
            List<CustomerResponse> customers = new();

            customers.Add(new()
            {
                Id = 1,
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            });

            customers.Add(new()
            {
                Id = 2,
                AddressName = "telegrafvej 12",
                CityName = "københavn",
                ZipCode = 2200
            });

            _mockcustomerService
                .Setup(x => x.GetAll()).ReturnsAsync(customers);
            //act
            var result = await _customerController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoCustomersExists()
        {

            //arrange
            List<CustomerResponse> customers = new();

            _mockcustomerService.Setup(x => x.GetAll()).ReturnsAsync(customers);
            //act
            var result = await _customerController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {

            //arrange
            _mockcustomerService.Setup(x => x.GetAll()).ReturnsAsync(() => null);
            //act
            var result = await _customerController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {

            //arrange
            _mockcustomerService.Setup(x => x.GetAll()).ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _customerController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            int customerId = 1;

            CustomerResponse customer = new()
            {
                Id = customerId,
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };

            _mockcustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(customer);
            //act
            var result = await _customerController.GetById(customerId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCustomerDoesNotExists()
        {
            int customerId = 1;
            _mockcustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _customerController.GetById(customerId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            _mockcustomerService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //act
            var result = await _customerController.GetById(1);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenCustomerIsSuccessfullyCreated()
        {
            NewCustomer newcustomer = new()
            {
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };
            int customerId = 1;

            CustomerResponse customerResponse = new()
            {
                Id = customerId,
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };

            _mockcustomerService
                .Setup(x => x.Create(It.IsAny<NewCustomer>()))
                .ReturnsAsync(customerResponse);

            //act
            var result = await _customerController.Create(newcustomer);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            NewCustomer newcustomer = new()
            {
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };
            _mockcustomerService
                .Setup(x => x.Create(It.IsAny<NewCustomer>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _customerController.Create(newcustomer);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCustomerIsSuccessfullyUpdated()
        {
            UpdateCustomer updatecustomer = new()
            {
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };
            int customerId = 1;

            CustomerResponse customerResponse = new()
            {
                Id = customerId,
                AddressName = "telegrafvej 50",
                CityName = "københavn",
                ZipCode = 2000
            };
            _mockcustomerService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCustomer>()))
                .ReturnsAsync(customerResponse);

            //act
            var result = await _customerController.Update(customerId, updatecustomer);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenTryingToUpdateCustomerWhichDoesNotExists()
        {
            UpdateCustomer updatecustomer = new()
            {
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };
            int customerId = 1;
            _mockcustomerService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCustomer>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _customerController.Update(customerId, updatecustomer);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenExceptionIsRaised()
        {
            UpdateCustomer updatecustomer = new()
            {
                AddressName = "telegrafvej 9",
                CityName = "københavn",
                ZipCode = 2200
            };

            int customerId = 1;
            _mockcustomerService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCustomer>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));
            //act
            var result = await _customerController.Update(customerId, updatecustomer);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenCustomerIsDeleted()
        {
            int customerId = 1;

            _mockcustomerService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _customerController.Delete(customerId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int customerId = 1;
            _mockcustomerService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _customerController.Delete(customerId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
