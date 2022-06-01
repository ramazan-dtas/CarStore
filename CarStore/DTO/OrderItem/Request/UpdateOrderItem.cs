using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.OrderItem.Request
{
    public class UpdateOrderItem
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int OrderlistId { get; set; }

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
