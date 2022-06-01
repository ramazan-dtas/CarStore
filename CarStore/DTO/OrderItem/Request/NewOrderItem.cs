using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.OrderItem.Request
{
    public class NewOrderItem
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int OrderListId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
