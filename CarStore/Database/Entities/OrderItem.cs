using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarStore.Database.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }



        [Required]
        public int Price { get; set; }



        [Required]
        public int Quantity { get; set; }


        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
