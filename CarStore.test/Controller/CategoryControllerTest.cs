using CarStore.Controllers;
using CarStore.DTO.Category.Request;
using CarStore.DTO.Category.Response;
using CarStore.Services.CategoryService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Controller
{
    public class CategoryControllerTest
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<ICategoryService> _mockcategoryService = new();

        public CategoryControllerTest()
        {
            _categoryController = new(_mockcategoryService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCategorysExists()
        {

            //arrange
            List<CategoryResponse> categorys = new();

            categorys.Add(new()
            {
                Id = 1,
                CategoryName = "Aston Martin"
            });

            categorys.Add(new()
            {
                Id = 2,
                CategoryName = "Rolls-Royce"
            });

            _mockcategoryService
                .Setup(x => x.GetAll()).ReturnsAsync(categorys);
            //act
            var result = await _categoryController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoCategorysExists()
        {

            //arrange
            List<CategoryResponse> categorys = new();

            _mockcategoryService.Setup(x => x.GetAll()).ReturnsAsync(categorys);
            //act
            var result = await _categoryController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {

            //arrange
            _mockcategoryService.Setup(x => x.GetAll()).ReturnsAsync(() => null);
            //act
            var result = await _categoryController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {

            //arrange
            _mockcategoryService.Setup(x => x.GetAll()).ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _categoryController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            int categoryId = 1;

            CategoryResponse category = new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            };

            _mockcategoryService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(category);
            //act
            var result = await _categoryController.GetById(categoryId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenCategoryDoesNotExists()
        {
            int categoryId = 1;
            _mockcategoryService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _categoryController.GetById(categoryId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            _mockcategoryService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //act
            var result = await _categoryController.GetById(1);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenCategoryIsSuccessfullyCreated()
        {
            NewCategory newcategory = new()
            {
                categoryName = "Aston Martin"
            };
            int categoryId = 1;

            CategoryResponse categoryResponse = new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            };

            _mockcategoryService
                .Setup(x => x.Create(It.IsAny<NewCategory>()))
                .ReturnsAsync(categoryResponse);

            //act
            var result = await _categoryController.Create(newcategory);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            NewCategory newcategory = new()
            {
                categoryName = "Aston Martin"
            };
            _mockcategoryService
                .Setup(x => x.Create(It.IsAny<NewCategory>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _categoryController.Create(newcategory);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenCategoryIsSuccessfullyUpdated()
        {
            UpdateCategory updatecategory = new()
            {
                categoryName = "Aston Martin"
            };
            int categoryId = 1;

            CategoryResponse categoryResponse = new()
            {
                Id = categoryId,
                CategoryName = "Aston Martin"
            };
            _mockcategoryService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCategory>()))
                .ReturnsAsync(categoryResponse);

            //act
            var result = await _categoryController.Update(categoryId, updatecategory);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenTryingToUpdatecategoryWhichDoesNotExists()
        {
            UpdateCategory updatecategory = new()
            {
                categoryName = "Aston Martin"
            };
            int categoryId = 1;
            _mockcategoryService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCategory>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _categoryController.Update(categoryId, updatecategory);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenExceptionIsRaised()
        {
            UpdateCategory updatecategory = new()
            {
                categoryName = "Aston Martin"
            };

            int categoryId = 1;
            _mockcategoryService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateCategory>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));
            //act
            var result = await _categoryController.Update(categoryId, updatecategory);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenCategoryIsDeleted()
        {
            int categoryId = 1;

            _mockcategoryService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _categoryController.Delete(categoryId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int categoryId = 1;
            _mockcategoryService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _categoryController.Delete(categoryId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
