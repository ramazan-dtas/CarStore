using CarStore.Database.Entities;

namespace CarStore.Repository.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> SelectAllOrderItems();
        Task<OrderItem> SelectOrderItemById(int orderItemId);
        Task<OrderItem> InsertNewOrderItem(OrderItem orderItem);
        Task<OrderItem> UpdateExistingOrderItem(int orderId, OrderItem orderItem);
        Task<OrderItem> DeleteOrderItem(int orderItemId);
    }
}
