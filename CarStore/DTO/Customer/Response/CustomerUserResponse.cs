using CarStore.Helpers;

namespace CarStore.DTO.Customer.Response
{
    public class CustomerUserResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
