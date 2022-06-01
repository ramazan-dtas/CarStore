using CarStore.Database.Entities;
using CarStore.DTO.Order.Request;
using CarStore.DTO.Order.Response;
using CarStore.Repository.OrderRepository;
using CarStore.Repository.UserRepository;

namespace CarStore.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderListRepository, IUserRepository userRepository)
        {
            _orderRepository = orderListRepository;
            _userRepository = userRepository;
        }
        public async Task<List<OrderResponse>> GetAll()
        {
            List<Order> order = await _orderRepository.SelectAllOrder();

            return order.Select(a => new OrderResponse
            {
                Id = a.Id,
                OrderDateTime = a.OrderDateTime,
                User = new OrderUserResponse
                {
                    UserId = a.User.Id,
                    Email = a.User.Email,
                    Role = a.User.Role
                }
            }).ToList();
        }
        public async Task<OrderResponse> GetById(int orderId)
        {
            Order order = await _orderRepository.SelectOrderById(orderId);
            return order == null ? null : new OrderResponse
            {
                Id = order.Id,
                OrderDateTime = order.OrderDateTime

            };
        }
        public async Task<OrderResponse> Create(NewOrder newOrder)
        {
            Order order = new Order
            {
                UserId = newOrder.UserId,
                OrderDateTime = newOrder.OrderDateTime

            };

            order = await _orderRepository.InsertNewOrder(order);

            return order == null ? null : new OrderResponse
            {
                Id = order.Id,
                OrderDateTime = order.OrderDateTime

            };
        }

        public async Task<OrderResponse> Update(int orderId, UpdateOrder updateOrder)
        {
            Order order = new Order
            {
                OrderDateTime = updateOrder.OrderDateTime,
                // UserIdxxx = updateOrder.UserId,
            };

            order = await _orderRepository.UpdateExistingOrder(orderId, order);

            return order == null ? null : new OrderResponse
            {
                Id = orderId,
                OrderDateTime = order.OrderDateTime
            };
        }
        public async Task<bool> Delete(int orderId)
        {
            var result = await _orderRepository.DeleteOrder(orderId);
            return (result != null);
        }
    }
}
