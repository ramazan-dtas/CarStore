using CarStore.Controllers;
using CarStore.DTO.User.Request;
using CarStore.DTO.User.Response;
using CarStore.Services.UserService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Controller
{
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockUserServices = new();
        public UserControllerTest()
        {
            _userController = new(_mockUserServices.Object);
        }

        [Fact]
        public async void GetAll_ShuldReturnStatusCode200_WhenUserExist()
        {

            //arrange
            List<UserResponse> userResponses = new();

            userResponses.Add(new()
            {
                Id = 1,
                Email = "test123@pikmail.com",
                Password = "1234567!",
                Role = 0
            });

            userResponses.Add(new()
            {
                Id = 2,
                Email = "test123@pikmail.com",
                Password = "123456e7!",
                Role = 0
            });

            _mockUserServices
                .Setup(x => x.GetAll())
                .ReturnsAsync(userResponses);
            //act
            var result = await _userController.GetAll();

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoUserExists()
        {
            //Arrange 
            List<UserResponse> users = new();

            _mockUserServices
                .Setup(x => x.GetAll())
                .ReturnsAsync(users);

            //Act 
            var result = await _userController.GetAll();

            //Assert 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, StatusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            //Arrange 
            _mockUserServices.Setup(x => x.GetAll()).ReturnsAsync(() => null);

            //Act
            var result = await _userController.GetAll();

            //Arrange 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arrange 
            _mockUserServices
                .Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            //act
            var result = await _userController.GetAll();

            //assert
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            //Arrange 
            int userId = 1;

            UserResponse user = new()
            {
                Id = userId,
                Email = "test@test.dk",
                Password = "test123",
                Role = 0
            };
            _mockUserServices
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(user);
            //Act
            var result = await _userController.GetById(userId);

            //Assert 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCategoryDoesNotExists()
        {
            //Arrange 
            int userId = 1;

            _mockUserServices
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            //act
            var result = await _userController.GetById(userId);
            //Arrange 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenUserIsSuccessfullyCreated()
        {
            //arrange 
            NewUser newUser = new()
            {
                Email = "test@test.dik",
                Password = "test3231",
            };
            int userId = 1;

            UserResponse userResponse = new()
            {
                Id = userId,
                Email = "test@test.de",
                Password = "test2111",
                Role = 0
            };
            _mockUserServices
                .Setup(x => x.Create(It.IsAny<NewUser>()))
                .ReturnsAsync(userResponse);

            //act
            var result = await _userController.Create(newUser);
            //Assert
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, StatusCodeResult.StatusCode);

        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {

            //Assert
            NewUser newUser = new()
            {
                Email = "Ramantes@tec.dk",
                Password = "tomsskilpade69"
            };

            _mockUserServices
                .Setup(x => x.Create(It.IsAny<NewUser>()))
                .ReturnsAsync(() => throw new System.Exception("ths is an ecxeption"));

            //Act
            var result = await _userController.Create(newUser);

            //Arrange 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);


        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUserIsSuccessfullyUpdated()
        {
            //Assert
            UpdateUser updateUser = new()
            {
                Email = "MNR@kæmpet.com",
                Password = "teslatesla"
            };

            int userId = 1;
            UserResponse userResponse = new()
            {
                Id = userId,
                Email = "skildpade@toms.toms",
                Password = ""
            };

            _mockUserServices
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateUser>()))
                .ReturnsAsync(userResponse);

            //Act
            var result = await _userController.Update(userId, updateUser);

            //Arrange
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, StatusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenTryingToUpdatecategoryWhichDoesNotExists()
        {
            //Arrange
            UpdateUser updateUser = new()
            {
                Email = "I<3AudiRS6@audi.dk",
                Password = "I<3MyaudiRS4"
            };
            int userId = 1;

            _mockUserServices
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateUser>()))
                .ReturnsAsync(() => null);

            //Act
            var result = await _userController.Update(userId, updateUser);

            //Assert
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);

        }


        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenExceptionIsRaised()
        {
            //Arrange 
            UpdateUser updateUser = new()
            {
                Email = "DinLocaleKurterDerStjæler@Nodea.dk",
                Password = "Densomsiggerundendeveddet"
            };
            int userId = 1;
            _mockUserServices
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateUser>()))
                .ReturnsAsync(() => throw new System.Exception("this is an ecxeption"));

            //Act
            var result = await _userController.Update(userId, updateUser);

            //Assert
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenCategoryIsDeleted()
        {
            //Arragne 
            int userId = 1;
            _mockUserServices
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await _userController.Delete(userId);

            //Assert
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, StatusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            //Arragne
            int userId = 1;
            _mockUserServices
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an excption"));

            //act
            var result = await _userController.Delete(userId);

            //assert 
            var StatusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, StatusCodeResult.StatusCode);

        }
    }
}
