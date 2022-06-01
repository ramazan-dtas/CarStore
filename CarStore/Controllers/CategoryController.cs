using CarStore.DTO.Category.Request;
using CarStore.DTO.Category.Response;
using CarStore.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CategoryResponse> CategoryResponses =
                    await _categoryService.GetAll();

                if (CategoryResponses == null)
                {
                    return Problem("Nothing...");
                }
                if (CategoryResponses.Count == 0)
                {
                    return NoContent();
                }
                return Ok(CategoryResponses);
            }
            catch (Exception exp)
            {
                return Problem(exp.Message);
            }
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            try
            {
                CategoryResponse CategoryResponse =
                    await _categoryService.GetById(Id);

                if (CategoryResponse == null)
                {
                    return Problem("Nothing...");
                }
                return Ok(CategoryResponse);
            }
            catch (Exception exp)
            {
                return Problem(exp.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewCategory newCategory)
        {
            try
            {
                CategoryResponse CategoryResponse =
                    await _categoryService.Create(newCategory);

                if (CategoryResponse == null)
                {
                    return Problem("Nothing...");
                }
                return Ok(CategoryResponse);
            }
            catch (Exception exp)
            {

                return Problem(exp.Message);
            }
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int Id,
        [FromBody] UpdateCategory updateCategory)
        {
            try
            {
                CategoryResponse CategoryResponse =
                    await _categoryService.Update(Id, updateCategory);

                if (CategoryResponse == null)
                {
                    return Problem("Nothing...");
                }

                return Ok(CategoryResponse);
            }
            catch (Exception exp)
            {

                return Problem(exp.Message);
            }
        }
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            try
            {
                bool result = await _categoryService.Delete(Id);

                if (!result)
                {
                    return Problem("Could not be deleted");
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
