using CarStore.Controllers;
using CarStore.DTO.Product.Request;
using CarStore.DTO.Product.Response;
using CarStore.Services.ProductService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.test.Controller
{
    public class ProductControllerTest
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _mockproductService = new();

        public ProductControllerTest()
        {
            _productController = new(_mockproductService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExists()
        {

            //arrange
            List<ProductResponse> products = new();

            products.Add(new()
            {
                Id = 1,
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            });

            products.Add(new()
            {
                Id = 2,
                ProductName = "Ghost",
                Price = 3345000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget flot"
            });

            _mockproductService
                .Setup(x => x.GetAll()).ReturnsAsync(products);
            //act
            var result = await _productController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoProductsExists()
        {

            //arrange
            List<ProductResponse> products = new();

            _mockproductService.Setup(x => x.GetAll()).ReturnsAsync(products);
            //act
            var result = await _productController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {

            //arrange
            _mockproductService.Setup(x => x.GetAll()).ReturnsAsync(() => null);
            //act
            var result = await _productController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {

            //arrange
            _mockproductService.Setup(x => x.GetAll()).ReturnsAsync(() => throw new Exception("This is an exception"));
            //act
            var result = await _productController.GetAll();
            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            int productId = 1;

            ProductResponse product = new()
            {
                Id = productId,
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };

            _mockproductService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(product);
            //act
            var result = await _productController.GetById(productId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductDoesNotExists()
        {
            int productId = 1;
            _mockproductService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _productController.GetById(productId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            _mockproductService
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an Exception"));

            //act
            var result = await _productController.GetById(1);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenProductIsSuccessfullyCreated()
        {
            NewProduct newproduct = new()
            {
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };
            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = productId,
                ProductName = "Db 12",
                Price = 3000000,
                ProductionYear = 2022,
                Km = 5,
                Description = "Den har mega meget sej"
            };

            _mockproductService
                .Setup(x => x.Create(It.IsAny<NewProduct>()))
                .ReturnsAsync(productResponse);

            //act
            var result = await _productController.Create(newproduct);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            NewProduct newproduct = new()
            {
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };
            _mockproductService
                .Setup(x => x.Create(It.IsAny<NewProduct>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _productController.Create(newproduct);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenProductIsSuccessfullyUpdated()
        {
            UpdateProduct updateproduct = new()
            {
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };
            int productId = 1;

            ProductResponse productResponse = new()
            {
                Id = productId,
                ProductName = "Db 12",
                Price = 3000000,
                ProductionYear = 2022,
                Km = 5,
                Description = "Den har mega meget sej"
            };
            _mockproductService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateProduct>()))
                .ReturnsAsync(productResponse);

            //act
            var result = await _productController.Update(productId, updateproduct);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenTryingToUpdateProductWhichDoesNotExists()
        {
            UpdateProduct updateproduct = new()
            {
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };
            int productId = 1;
            _mockproductService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateProduct>()))
                .ReturnsAsync(() => null);

            //act
            var result = await _productController.Update(productId, updateproduct);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenExceptionIsRaised()
        {
            UpdateProduct updateproduct = new()
            {
                ProductName = "Db 11",
                Price = 1500000,
                ProductionYear = 2021,
                Km = 5,
                Description = "Den har meget sej"
            };

            int productId = 1;
            _mockproductService
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<UpdateProduct>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));
            //act
            var result = await _productController.Update(productId, updateproduct);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenProductIsDeleted()
        {
            int productId = 1;

            _mockproductService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _productController.Delete(productId);

            // Assert 
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            int productId = 1;
            _mockproductService
                .Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("this is an exception"));

            //act
            var result = await _productController.Delete(productId);

            //assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
