using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.Order.Request
{
    public class NewOrder
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public DateTime OrderDateTime { get; set; }
    }
}
