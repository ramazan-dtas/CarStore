using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AbContext _context;


        public UserRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<User> DeleteUser(int userId)
        {
            User deleteUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (deleteUser != null)
            {
                _context.User.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
            return deleteUser;
        }

        public async Task<User> InsertNewUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> SelectAllUsers()
        {
            return await _context.User.Include(a => a.Customer).Include(a => a.Order).ToListAsync();
        }

        public async Task<User> SelectUserById(int userId)
        {
            return await _context.User
                .Include(a => a.Customer)
                .Include(b => b.Order)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> UpdateExistingUser(int userId, User user)
        {
            User updateUser = await _context.User
                .FirstOrDefaultAsync(user => user.Id == userId);
            if (updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.Password = user.Password;
                updateUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }
    }
}
