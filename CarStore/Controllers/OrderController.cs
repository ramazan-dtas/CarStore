using CarStore.DTO.Order.Request;
using CarStore.DTO.Order.Response;
using CarStore.Services.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService ordersService)
        {
            _orderService = ordersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<OrderResponse> OrderList = await _orderService.GetAll();

                if (OrderList == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (OrderList.Count == 0)
                {
                    return NoContent();
                }

                return Ok(OrderList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int OrderListId)
        {
            try
            {
                OrderResponse Orders = await _orderService.GetById(OrderListId);

                if (Orders == null)
                {
                    return NotFound();
                }

                return Ok(Orders);
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
        public async Task<IActionResult> Create([FromBody] NewOrder newOrder)
        {
            try
            {
                OrderResponse Orders = await _orderService.Create(newOrder);

                if (Orders == null)
                {
                    return Problem("Product was not created, something went wrong");
                }

                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int OrderListId, [FromBody] UpdateOrder updateOrder)
        {
            try
            {
                OrderResponse Orders = await _orderService.Update(OrderListId, updateOrder);

                if (Orders == null)
                {
                    return Problem("Product was not updated, something went wrong");
                }

                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{OrderListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int OrderListId)
        {
            try
            {
                bool result = await _orderService.Delete(OrderListId);

                if (!result)
                {
                    return Problem("Order was not deleted, something went wrong");
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
