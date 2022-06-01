namespace CarStore.DTO.Order.Response
{
    public class OrderOrderItemResponse
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
