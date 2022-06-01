using CarStore.Database.Entities;
using CarStore.DTO.User.Request;
using CarStore.DTO.User.Response;
using CarStore.Helpers;
using CarStore.Repository.UserRepository;

namespace CarStore.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        private UserResponse userResponse(User user)
        {
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _userRepository.SelectAllUsers();
            return users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                Order = u.Order.Select(o => new UserOrderResponse
                {
                    OrderId = o.Id,
                    OrderDateTime = o.OrderDateTime
                }).ToList(),
                Customers = u.Customer.Select(a => new UserCostumerResponse
                {
                    CustomerId = a.Id,
                    AddressName = a.AddressName,
                    ZipCode = a.ZipCode,
                    CityName = a.CityName
                }).ToList()
            }).ToList();
        }
        public async Task<UserResponse> GetById(int userId)
        {
            User user = await _userRepository.SelectUserById(userId);
            return userResponse(user);
        }
        public async Task<UserResponse> Create(NewUser newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                Password = newUser.Password,
                Role = Role.User // Alle nye users får automatisk user rollen
            };
            user = await _userRepository.InsertNewUser(user);
            return userResponse(user);
        }
        public async Task<UserResponse> Update(int userId, UpdateUser updateUser)
        {
            User user = new User
            {
                Email = updateUser.Email,
                Password = updateUser.Password,
                Role = updateUser.Role
            };
            user = await _userRepository.UpdateExistingUser(userId, user);
            return userResponse(user);
        }
        public async Task<bool> Delete(int userId)
        {
            var result = await _userRepository.DeleteUser(userId);
            if (result != null) return true;
            else return false;
        }
    }
}
