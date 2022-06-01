using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarStore.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string ProductName { get; set; }


        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }


        [Required]
        public int ProductionYear { get; set; }


        [Required]
        public int Km { get; set; }


        [Column(TypeName = "nvarchar(528)")]
        public string Description { get; set; }


        [ForeignKey("Category.Id")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItem { get; set; } = new();
    }
}
