using CarStore.Database;
using CarStore.Database.Entities;
using CarStore.Repository.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Repository
{
    public class UserRepositoryTest
    {
        private readonly DbContextOptions<AbContext> _options;
        private readonly AbContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<AbContext>()
                .UseInMemoryDatabase(databaseName: "CarStoreUser")
                .Options;

            _context = new(_options);
            _userRepository = new(_context);
        }

        [Fact]
        public async void SelectAllUser_ShouldReturnListOfUsers_WhenUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(new()
            {
                Id = 1,
                Email = "PlumErMinBitch@gmail.com",
                Password = "123",
                Role = 0
            });
            _context.User.Add(new()
            {
                Id = 2,
                Email = "NiklasErMinSideChick@gmail.com",
                Password = "123",
                Role = 0

            });

            await _context.SaveChangesAsync();
            //act
            var result = await _userRepository.SelectAllUsers();

            //assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllUsers_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.SelectAllUsers();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            _context.User.Add(new()
            {
                Id = userId,
                Email = "NiklasErMinSideChick@gmail.com",
                Password = "123",
                Role = 0
            });

            await _context.SaveChangesAsync();

            //Act
            var result = await _userRepository.SelectUserById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async void SelectUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.SelectUserById(1);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewUser_ShouldAddNewIdToUser_WhenSavingToDatabase()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            User user = new()
            {
                Email = "NiklasErMinSideChick@gmail.com",
                Password = "123",
                Role = 0
            };

            //Act
            var result = await _userRepository.InsertNewUser(user);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result.Id);
        }

        [Fact]
        public async void InsertNewUser_ShouldFailToAddNewUser_WhenUserIdAlreadyExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            User user = new()
            {
                Id = 1,
                Email = "NiklasErMinSideChick@gmail.com",
                Password = "123",
                Role = 0
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            //Act
            async Task action() => await _userRepository.InsertNewUser(user);

            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingUser_ShouldChangeValuesOnUser_WhenUserExists()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newuser = new()
            {
                Id = userId,
                Email = "NiklasErMinSideChick@gmail.com",
                Password = "123",
                Role = 0
            };

            _context.User.Add(newuser);
            await _context.SaveChangesAsync();

            User updateuser = new()
            {
                Id = userId,
                Email = "PlumErMinBitch@gmail.com",
                Password = "123",
                Role = 0
            };

            //Act
            var result = await _userRepository.UpdateExistingUser(userId, updateuser);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateuser.Email, result.Email);
        }

        [Fact]
        public async void UpdateExistingUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User updateuser = new()
            {
                Id = userId,
                Email = "PlumErMinBitch@gmail.com",
                Password = "123",
                Role = 0
            };

            //Act
            var result = await _userRepository.UpdateExistingUser(userId, updateuser);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletedUserById_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newuser = new()
            {
                Email = "PlumErMinBitch@gmail.com",
                Password = "123",
                Role = 0
            };

            _context.User.Add(newuser);
            await _context.SaveChangesAsync();

            //Act
            var result = await _userRepository.DeleteUser(userId);
            var user = await _userRepository.SelectUserById(userId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Null(user);
        }

        [Fact]
        public async void DeleteUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();

            //Act
            var result = await _userRepository.DeleteUser(1);

            //Assert
            Assert.Null(result);
        }
    }
}
