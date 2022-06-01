using CarStore.Helpers;

namespace CarStore.DTO.Order.Response
{
    public class OrderUserResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
