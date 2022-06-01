using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarStore.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }



        [Required]
        public DateTime OrderDateTime { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }



        public List<OrderItem> OrderItem { get; set; } = new();
    }
}
