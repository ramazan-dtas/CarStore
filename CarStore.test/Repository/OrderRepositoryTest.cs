using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Repository.OrderRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class OrderRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreOrder")
                .Options;

            _context = new(_options);
            _orderRepository = new(_context);
        }

        [Fact]
        public async void SelectAllOrder_ShouldReturnListOfOrders_WhenOrdersExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Order.Add(new Order
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),
                User = new()

            });
            _context.Order.Add(new Order
            {
                Id = 2,
                OrderDateTime = DateTime.Parse("2020-12-21 12:57:00"),
                User = new()

            });
            await _context.SaveChangesAsync();
            int expectedSize = 2;

            // Act
            var result = await _orderRepository.SelectAllOrder();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Equal(expectedSize, result.Count);
        }

        [Fact]
        public async void SelectAllOrders_ShouldReturnEmptyListOfOrders_WhenNoOrdersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderRepository.SelectAllOrder();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Order>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int OrderId = 1;

            _context.Order.Add(new Order
            {
                Id = OrderId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _orderRepository.SelectOrderById(OrderId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(OrderId, result.Id);
        }

        [Fact]
        public async void SelectOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderRepository.SelectOrderById(1);

            //Assert
            Assert.Null(result);
            //Assert
        }

        [Fact]
        public async void InsertNewOrder_ShouldAddNewIdToOrder_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Order order = new()
            {
                //orderName = "Aston Martin"
            };

            //Act
            var result = await _orderRepository.InsertNewOrder(order);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewOrder_ShouldFailToAddNewOrder_WhenOrderIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Order Order_Lists = new Order
            {
                Id = 1,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")
            };

            _context.Order.Add(Order_Lists);
            await _context.SaveChangesAsync();

            // act
            Func<Task> action = async () => await _orderRepository.InsertNewOrder(Order_Lists);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingOrder_ShouldChangeValuesOnOrder_WhenOrderExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderId = 1;
            Order Order = new Order
            {
                Id = OrderId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")

            };
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            Order updateOrder = new Order
            {
                Id = OrderId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00"),

            };

            // Act
            var result = await _orderRepository.UpdateExistingOrder(OrderId, updateOrder);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(OrderId, result.Id);
            Assert.Equal(updateOrder.OrderDateTime, result.OrderDateTime);
        }

        [Fact]
        public async void UpdateExistingOrder_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int orderId = 1;

            Order updateorder = new()
            {
                Id = orderId,
                //orderName = "Aston Martin"
            };

            //Act
            var result = await _orderRepository.UpdateExistingOrder(orderId, updateorder);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedOrder_WhenOrder_ListsIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int OrderId = 1;
            Order order = new Order
            {
                Id = OrderId,
                OrderDateTime = DateTime.Parse("2021-12-21 12:55:00")

            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            var result = await _orderRepository.DeleteOrder(OrderId);
            var Order = await _orderRepository.SelectAllOrder();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Order>(result);
            Assert.Equal(OrderId, result.Id);

            Assert.Empty(Order);
        }

        [Fact]
        public async void DeleteOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderRepository.DeleteOrder(1);

            //Assert
            Assert.Null(result);
        }
    }
}
