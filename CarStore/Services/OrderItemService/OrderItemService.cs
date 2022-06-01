using CarStore.Database.Entities;
using CarStore.DTO.OrderItem.Request;
using CarStore.DTO.OrderItem.Response;
using CarStore.Repository.OrderItemRepository;
using CarStore.Repository.OrderRepository;
using CarStore.Repository.ProductRepository;

namespace CarStore.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository
            )
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<List<OrderItemResponse>> GetAll()
        {
            List<OrderItem> orderItems = await _orderItemRepository.SelectAllOrderItems();
            return orderItems.Select(o => new OrderItemResponse
            {
                Id = o.Id,
                Price = o.Price,
                Quantity = o.Quantity
            }).ToList();
        }
        public async Task<OrderItemResponse> GetById(int orderItemsId)
        {
            OrderItem orderItem = await _orderItemRepository.SelectOrderItemById(orderItemsId);
            return orderItem == null ? null : new OrderItemResponse
            {
                Id = orderItem.Id,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
                Order = new OrderItemOrderResponse
                {
                    OrderId = orderItem.Order.Id,
                    OrderDateTime = orderItem.Order.OrderDateTime
                },
                Products = new OrderItemProductResponse
                {
                    ProductId = orderItem.Product.Id,
                    ProductName = orderItem.Product.ProductName,
                    Price = orderItem.Product.Price,
                    ProductionYear = orderItem.Product.ProductionYear,
                    Km = orderItem.Product.Km,
                    Description = orderItem.Product.Description
                }
            };
        }
        public async Task<OrderItemResponse> Create(NewOrderItem newOrderItem)
        {
            OrderItem orderItem = new OrderItem
            {
                Price = newOrderItem.Price,
                Quantity = newOrderItem.Quantity,
                OrderId = newOrderItem.OrderListId,
                ProductId = newOrderItem.ProductId
            };

            orderItem = await _orderItemRepository.InsertNewOrderItem(orderItem);
            if (orderItem == null) return null;
            else
            {
                await _orderRepository.SelectOrderById(orderItem.Id);
                await _productRepository.SelectProductById(orderItem.ProductId);
                return new OrderItemResponse
                {
                    Id = orderItem.Id,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    Order = new OrderItemOrderResponse
                    {
                        OrderId = orderItem.Order.Id,
                        OrderDateTime = orderItem.Order.OrderDateTime
                    },
                    Products = new OrderItemProductResponse
                    {
                        ProductId = orderItem.Product.Id,
                        ProductName = orderItem.Product.ProductName,
                        Price = orderItem.Product.Price,
                        ProductionYear = orderItem.Product.ProductionYear,
                        Km = orderItem.Product.Km,
                        Description = orderItem.Product.Description
                    }
                };
            }
        }
        public async Task<OrderItemResponse> Update(int orderItemsId, UpdateOrderItem updateOrderItem)
        {
            OrderItem orderItem = new OrderItem
            {
                Price = updateOrderItem.Price,
                Quantity = updateOrderItem.Quantity,
                OrderId = updateOrderItem.OrderlistId,
                ProductId = updateOrderItem.ProductId
            };
            orderItem = await _orderItemRepository.UpdateExistingOrderItem(orderItemsId, orderItem);
            if (orderItem == null) return null;
            else
            {
                await _orderRepository.SelectOrderById(orderItem.Id);
                await _productRepository.SelectProductById(orderItem.ProductId);
                return new OrderItemResponse
                {
                    Id = orderItem.Id,
                    Price = orderItem.Price,
                    Quantity = orderItem.Quantity,
                    Order = new OrderItemOrderResponse
                    {
                        OrderId = orderItem.Order.Id,
                        OrderDateTime = orderItem.Order.OrderDateTime
                    },
                    Products = new OrderItemProductResponse
                    {
                        ProductId = orderItem.Product.Id,
                        ProductName = orderItem.Product.ProductName,
                        Price = orderItem.Product.Price,
                        ProductionYear = orderItem.Product.ProductionYear,
                        Km = orderItem.Product.Km,
                        Description = orderItem.Product.Description
                    }
                };
            }
        }

        public Task<OrderItemResponse> Delete(int orderItemId)
        {
            throw new NotImplementedException();
        }
    }
}
