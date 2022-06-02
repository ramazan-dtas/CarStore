using CarStore.Database.Entities;
using CarStore.DTO.User.Request;
using CarStore.DTO.User.Response;
using CarStore.Helpers;
using CarStore.Repository.CustomerRepository;
using CarStore.Repository.UserRepository;
using CarStore.Services.UserService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Service
{
    public class UserServiceTest
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _userRepository = new();
        private readonly Mock<ICustomerRepository> _customerRepository = new();

        public UserServiceTest()
        {
            _sut = new UserService(_userRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfCustomeresResponses_WhenCustomeresExist()
        {
            //Arrange 
            List<User> users = new();
            users.Add(new()
            {
                Id = 1,
                Email = "test@test.dk",
                Role = Role.User,
                Order = new List<Order> { },
                Customer = new List<Customer> { }

            });

            users.Add(new()
            {
                Id = 2,
                Email = "testr@test.com",
                Password = "test2311",
                Order = new List<Order> { },
                Customer = new List<Customer> { }
            });

            _userRepository
                .Setup(x => x.SelectAllUsers())
                .ReturnsAsync(users);

            //Act
            var result = await _sut.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserResponse>>(result);
        }
        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUserResponses_WhenNoUserExists()
        {
            //assert
            List<User> users = new List<User>();
            _userRepository
                .Setup(x => x.SelectAllUsers())
                .ReturnsAsync(users);

            //act
            var result = await _sut.GetAll();
            //arrange 
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UserResponse>>(result);


        }

        [Fact]
        public async void GetById_ShouldReturnAnUserResponse_WhenUserExists()
        {
            //Arragne 
            int id = 1;

            User user = new User
            {
                Id = 1,
                Email = "msflsdf@mfmfm.dk",
                Password = "lnflndsflknlkn1234"
            };

            _userRepository
                .Setup(x => x.SelectUserById(It.IsAny<int>()))
                .ReturnsAsync(user);

            //act
            var result = await _sut.GetById(id);

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //arrange 
            int userId = 1;

            _userRepository
                .Setup(x => x.SelectUserById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //act
            var result = await _sut.GetById(userId);
            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnUserResponse_WhenCreateIsSuccess()
        {
            //Arrange 
            User user = new User
            {
                Id = 1,
                Email = "tnlksdnflk@nlknldf.dk",
                Password = "lkndflnslnf"
            };

            NewUser newUser = new NewUser
            {
                Id = 1,
                Email = "sndflkndslkfndsf@mgngng.com",
                Password = "nflknslf"
            };

            _userRepository
                .Setup(x => x.InsertNewUser(It.IsAny<User>()))
                .ReturnsAsync(user);
            //Act
            var result = await _sut.Create(newUser);
            //Assert 
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);


        }

        [Fact]
        public async void Update_ShouldReturnUpdatedUserResponse_WhenUpdateIsSuccess()
        {
            //Arrange 
            int userId = 1;

            UpdateUser updateUser = new()
            {
                Email = "flkdnflk@mlkdfls.do",
                Password = "lsdfndskjfnkjndjjjjd",
                Role = Role.User,
            };

            User user = new User
            {
                Id = userId,
                Email = "flkdnflk@mlkdfls.do",
                Role = Role.User,
            };

            _userRepository.Setup(u => u.UpdateExistingUser(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(user);
            //Act
            var result = await _sut.Update(userId, updateUser);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateUser.Email, result.Email);
            Assert.Equal(updateUser.Role, result.Role);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            //Arrane 
            int id = 1;

            UpdateUser updateUser = new UpdateUser
            {
                Email = "mfmfmf@fmfmf.dk",
                Password = "fmfmmf",
                Role = Role.User,
            };

            _userRepository
                .Setup(x => x.UpdateExistingUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(() => null);
            //act
            var reuslt = await _sut.Update(id, updateUser);

            //Assert
            Assert.Null(reuslt);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            int userid = 1;

            User user = new User
            {
                Id = 1,
                Email = "fmfmfmmf@fff.dk",
                Password = "fffffffffff",
                Role = Role.User,
            };

            _userRepository.Setup(x => x.DeleteUser(It.IsAny<int>())).ReturnsAsync(() => null);
            //act

            var result = await _sut.Delete(userid);

            //Assert
            Assert.False(result);
        }
    }
}
