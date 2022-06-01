using CarStore.DTO.OrderItem.Request;
using CarStore.DTO.OrderItem.Response;

namespace CarStore.Services.OrderItemService
{
    public interface IOrderItemService
    {
        Task<List<OrderItemResponse>> GetAll();
        Task<OrderItemResponse> GetById(int orderItemId);
        Task<OrderItemResponse> Create(NewOrderItem newOrderItem);
        Task<OrderItemResponse> Update(int orderItemId, UpdateOrderItem updateOrderItem);
        Task<OrderItemResponse> Delete(int orderItemId);
    }
}
