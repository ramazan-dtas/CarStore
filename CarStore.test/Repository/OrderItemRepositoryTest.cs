using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Helpers;
using CarStore.Repository.OrderItemRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class OrderItemRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly OrderItemRepository _orderItemRepository;

        public OrderItemRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreOrderItem")
                .Options;

            _context = new(_options);
            _orderItemRepository = new(_context);
        }

        [Fact]
        public async void SelectAllOrderItem_ShouldReturnListOfOrderItems_WhenOrderItemsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.OrderItem.Add(new OrderItem
            {
                Id = 1,
                Price = 750000,
                Quantity = 2,
                Order = new Order
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    User = new User
                    {
                        Id = 1,
                        Email = "admin@admins.com",
                        Password = "Passw0rd",
                        Role = Role.Admin,
                    }
                },
                Product = new Product
                {
                    Id = 1,
                    ProductName = "McLaren 720s",
                    Price = 750000,
                    ProductionYear = 35000,
                    Km = 1,
                    Description = "Description",
                    Category = new Category
                    {
                        Id = 1,
                        CategoryName = "McLaren"
                    }
                }
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _orderItemRepository.SelectAllOrderItems();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderItem>>(result);
        }

        [Fact]
        public async void SelectAllOrderItems_ShouldReturnEmptyListOfOrderItems_WhenNoOrderItemsExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderItemRepository.SelectAllOrderItems();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<OrderItem>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectOrderItemById_ShouldReturnOrderItem_WhenOrderItemExists()
        {

            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            _context.OrderItem.Add(new OrderItem
            {
                Id = 1,
                Price = 100000000,
                Quantity = 2,
                Order = new Order
                {
                    Id = 1,
                    OrderDateTime = DateTime.Now,
                    User = new User
                    {
                        Id = 1,
                        Email = "admin@admins.com",
                        Password = "Passw0rd",
                        Role = Role.Admin,
                    }
                },
                Product = new Product
                {
                    Id = 1,
                    ProductName = "McLaren 720s",
                    Price = 100000,
                    ProductionYear = 2020,
                    Km = 1,
                    Description = "Description",
                    Category = new Category
                    {
                        Id = 1,
                        CategoryName = "McLaren"
                    }
                }
            });
            await _context.SaveChangesAsync();
            // Act
            var result = await _orderItemRepository.SelectOrderItemById(orderItemId);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(orderItemId, result.Id);

        }
        [Fact]
        public async void SelectOrderItemById_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderItemRepository.SelectOrderItemById(1);

            //Assert
            Assert.Null(result);
            //Assert
        }

        [Fact]
        public async void InsertNewOrderItem_ShouldAddNewIdToOrderItem_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            OrderItem orderItem = new()
            {
                Price = 300,
                Quantity = 10
            };

            //Act
            var result = await _orderItemRepository.InsertNewOrderItem(orderItem);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewOrderItem_ShouldFailToAddNewOrderItem_WhenOrderItemIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            OrderItem orderItem = new()
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                Order = new Order
                { },
                Product = new Product
                { }
            };
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            // Act
            Func<Task> action = async () => await _orderItemRepository.InsertNewOrderItem(orderItem);
            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnOrderItems_WhenOrderItemsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int orderItemId = 1;
            OrderItem orderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 1,
                Quantity = 1,
            };
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();
            OrderItem updateOrderItem = new OrderItem
            {
                Id = orderItemId,
                Price = 2,
                Quantity = 2,
            };
            // Act
            var result = await _orderItemRepository.UpdateExistingOrderItem(orderItemId, updateOrderItem);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItem>(result);
            Assert.Equal(orderItemId, result.Id);
            Assert.Equal(updateOrderItem.Price, result.Price);
            Assert.Equal(updateOrderItem.Quantity, result.Quantity);
        }

        [Fact]
        public async void UpdateExistingOrderItem_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int orderItemId = 1;

            OrderItem updateOrderItem = new()
            {
                Id = orderItemId,
                Price = 300
            };

            //Act
            var result = await _orderItemRepository.UpdateExistingOrderItem(orderItemId, updateOrderItem);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteOrderItemById_ShouldReturnNull_WhenOrderItemDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _orderItemRepository.DeleteOrderItem(1);

            //Assert
            Assert.Null(result);
        }
    }
}
