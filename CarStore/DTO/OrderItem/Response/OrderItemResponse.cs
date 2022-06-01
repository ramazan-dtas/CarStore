namespace CarStore.DTO.OrderItem.Response
{
    public class OrderItemResponse
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public OrderItemProductResponse Products { get; set; }

        public OrderItemOrderResponse Order { get; set; }
    }
}
