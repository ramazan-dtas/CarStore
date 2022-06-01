using CarStore.Database.Entities;

namespace CarStore.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> SelectAllUsers();
        Task<User> SelectUserById(int userId);
        Task<User> InsertNewUser(User user);
        Task<User> UpdateExistingUser(int userId, User user);
        Task<User> DeleteUser(int userId);
    }
}
