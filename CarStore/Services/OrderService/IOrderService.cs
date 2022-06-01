using CarStore.DTO.Order.Request;
using CarStore.DTO.Order.Response;

namespace CarStore.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAll();
        Task<OrderResponse> GetById(int orderlistId);
        Task<OrderResponse> Create(NewOrder newOrder);
        Task<OrderResponse> Update(int orderId, UpdateOrder updateOrder);
        Task<Boolean> Delete(int orderId);
    }
}
