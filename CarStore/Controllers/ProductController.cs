using CarStore.DTO.Product.Request;
using CarStore.DTO.Product.Response;
using CarStore.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ProductResponse> Products = await _productService.GetAll();

                if (Products == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (Products.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Products);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /*[HttpGet("Products/by_category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsByCategory([FromRoute] int categoryId)
        {
            try
            {
                var Products = await _productService.GetAllProductsByCategory(categoryId);
                return Ok(Products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }*/

        [HttpGet("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int ProductId)
        {
            try
            {
                ProductResponse Products = await _productService.GetById(ProductId);

                if (Products == null)
                {
                    return NotFound();
                }

                return Ok(Products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewProduct newProduct)
        {
            try
            {
                ProductResponse Products = await _productService.Create(newProduct);

                if (Products == null)
                {
                    return Problem("Product was not created, something went wrong");
                }

                return Ok(Products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int ProductId, [FromBody] UpdateProduct updateProduct)
        {
            try
            {
                ProductResponse Products = await _productService.Update(ProductId, updateProduct);

                if (Products == null)
                {
                    return Problem("Product was not updated, something went wrong");
                }

                return Ok(Products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{ProductId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int ProductId)
        {
            try
            {
                bool result = await _productService.Delete(ProductId);

                if (!result)
                {
                    return Problem("Product was not deleted, something went wrong");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
