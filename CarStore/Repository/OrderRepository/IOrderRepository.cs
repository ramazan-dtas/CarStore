using CarStore.Database.Entities;

namespace CarStore.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        Task<List<Order>> SelectAllOrder();
        Task<Order> SelectOrderById(int orderId);
        Task<Order> InsertNewOrder(Order order);
        Task<Order> UpdateExistingOrder(int orderId, Order order);
        Task<Order> DeleteOrder(int orderId);
    }
}
