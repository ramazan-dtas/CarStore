using CarStore.Helpers;

namespace CarStore.DTO.User.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public List<UserOrderResponse> Order { get; set; } = new();

        public List<UserCostumerResponse> Customers { get; set; } = new();
    }
}
