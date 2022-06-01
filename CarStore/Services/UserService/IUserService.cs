using CarStore.DTO.User.Request;
using CarStore.DTO.User.Response;

namespace CarStore.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int userId);
        Task<UserResponse> Create(NewUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
        Task<bool> Delete(int userId);
    }
}
