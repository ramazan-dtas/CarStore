using CarStore.Controllers;
using CarStore.DTO.Order.Request;
using CarStore.DTO.Order.Response;
using CarStore.Services.OrderService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Controller
{
    public class OrderControllerTest
    {
        private readonly OrderController _orderController;
        private readonly Mock<IOrderService> _mockorderService = new();

        public OrderControllerTest()
        {
            _orderController = new(_mockorderService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenOrdersExists()
        {

            //arrange
            List<OrderResponse> orders = new();

            orders.Add(new()
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new()
            });

            orders.Add(new()
            {
                Id = 2,
                OrderDateTime = DateTime.Parse("2021-12-19 12:55:00"),
                User = new()
            });

            _mockorderService
                .Setup(x => x.GetAll()).ReturnsAsync(orders);
            //act
            var result = await _orderController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoOrdersExists()
        {

            //arrange
            List<OrderResponse> orders = new();

            _mockorderService.Setup(x => x.GetAll()).ReturnsAsync(orders);
            //act
            var result = await _orderController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {

            //arrange
            _mockorderService.Setup(x => x.GetAll()).ReturnsAsync(() => null);
            //act
            var result = await _orderController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {

            //arrange
            _mockorderService.Setup(x => x.GetAll()).ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _orderController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            int orderId = 1;

            OrderResponse order = new()
            {
                Id = orderId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new()
            };

            _mockorderService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(order);
            //act
            var result = await _orderController.GetById(orderId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenOrderDoesNotExists()
        {
            int orderId = 1;
            _mockorderService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _orderController.GetById(orderId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            _mockorderService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //act
            var result = await _orderController.GetById(1);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenOrderIsSuccessfullyCreated()
        {
            NewOrder neworder = new()
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
            };
            int orderId = 1;

            OrderResponse orderResponse = new()
            {
                Id = orderId,
                OrderDateTime = DateTime.Parse("2022-01-20 12:55:00"),
                User = new()
            };

            _mockorderService
                .Setup(x => x.Create(It.IsAny<NewOrder>()))
                .ReturnsAsync(orderResponse);

            //act
            var result = await _orderController.Create(neworder);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            NewOrder neworder = new()
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                UserId = 1
            };
            _mockorderService
                .Setup(x => x.Create(It.IsAny<NewOrder>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _orderController.Create(neworder);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenOrderIsSuccessfullyUpdated()
        {
            UpdateOrder updateorder = new()
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };
            int orderId = 1;

            OrderResponse orderResponse = new()
            {
                Id = orderId,
                OrderDateTime = DateTime.Parse("2021-12-22 12:55:00"),
                User = new()
            };
            _mockorderService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateOrder>()))
                .ReturnsAsync(orderResponse);

            //act
            var result = await _orderController.Update(orderId, updateorder);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenTryingToUpdateOrderWhichDoesNotExists()
        {
            UpdateOrder updateorder = new()
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
            };
            int orderId = 1;
            _mockorderService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateOrder>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _orderController.Update(orderId, updateorder);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenExceptionIsRaised()
        {
            UpdateOrder updateorder = new()
            {
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
            };

            int orderId = 1;
            _mockorderService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateOrder>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));
            //act
            var result = await _orderController.Update(orderId, updateorder);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenOrderIsDeleted()
        {
            int orderId = 1;

            _mockorderService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _orderController.Delete(orderId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int orderId = 1;
            _mockorderService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _orderController.Delete(orderId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
