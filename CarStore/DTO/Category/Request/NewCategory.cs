using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.Category.Request
{
    public class NewCategory
    {
        [Required]
        [StringLength(255, ErrorMessage = "Max string length is 255")]
        [MinLength(1, ErrorMessage = "Min string length is 1")]
        public string categoryName { get; set; }
    }
}
