using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.Product.Request
{
    public class UpdateProduct
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The ProductName is to long ")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, 2500, ErrorMessage = "The ProductYear must be between 1 and 2500")]
        public int ProductionYear { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "The the km is to long we don't buy crapy cars ")]
        public int Km { get; set; }

        [Required]
        [StringLength(1111, ErrorMessage = "The Description is to long shortend it ")]
        public string Description { get; set; }
    }
}
