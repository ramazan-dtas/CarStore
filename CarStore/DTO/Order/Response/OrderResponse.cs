namespace CarStore.DTO.Order.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public DateTime OrderDateTime { get; set; }

        public List<OrderOrderItemResponse> OrderListOrderItem { get; set; }

        public OrderUserResponse User { get; set; }
    }
}
