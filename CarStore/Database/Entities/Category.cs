using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarStore.Database.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }



        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string CategoryName { get; set; }


        [JsonIgnore]
        public List<Product> Product { get; set; } = new();
    }
}
