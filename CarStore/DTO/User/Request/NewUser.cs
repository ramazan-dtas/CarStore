using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.User.Request
{
    public class NewUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Email must be less than 255 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Password must be less than 255 chars")]
        public string Password { get; set; }

    }
}
