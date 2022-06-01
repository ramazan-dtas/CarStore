using CarStore.DTO.Customer.Request;
using CarStore.DTO.Customer.Response;
using CarStore.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerservice)
        {
            _customerService = customerservice;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CustomerResponse> customerResponses = await _customerService.GetAll();

                if (customerResponses == null)
                {
                    string problem = "Got no data, not even an empty list, this is unexpected";
                    return Problem(problem);
                }

                if (customerResponses.Count == 0) return NoContent();

                return Ok(customerResponses);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // GetById
        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int customerId)
        {
            try
            {
                CustomerResponse address = await _customerService.GetById(customerId);

                if (address == null)
                {
                    return NotFound();
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewCustomer newCustomer)
        {
            try
            {
                CustomerResponse address = await _customerService.Create(newCustomer);

                if (address == null)
                {
                    return Problem("Address was not created, something went wrong");
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // Update
        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] UpdateCustomer updateCustomer)
        {
            try
            {
                CustomerResponse addressResponse = await _customerService.Update(customerId, updateCustomer);

                if (addressResponse == null)
                {
                    return Problem("Customer was not updated, something went wrong");
                }

                return Ok(addressResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Delete
        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int customerId)
        {
            try
            {
                bool result = await _customerService.Delete(customerId);

                if (!result)
                {
                    return Problem("Customer was not deleted, something went wrong");
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
