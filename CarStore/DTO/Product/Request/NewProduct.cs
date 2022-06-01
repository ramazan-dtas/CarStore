using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.Product.Request
{
    public class NewProduct
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max string length is 255")]
        [MinLength(1, ErrorMessage = "Min string length is 1")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, 2500, ErrorMessage = "Please write a valid ProductionYear")]
        public int ProductionYear { get; set; }

        [Range(1, 200000, ErrorMessage = "The km is to high")]
        public int Km { get; set; }

        [StringLength(255, ErrorMessage = "Max string length is 255")]
        public string Description { get; set; }
    }
}
