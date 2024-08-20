using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> InitializeOrder(OrderDTO orderDTO)
        {
            try
            {
                var order = await _orderService.InitializeOrder(orderDTO.WaiterId, orderDTO.TableId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderById(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("{orderId}/addDish")]
        public async Task<ActionResult> AddDishToOrder(int orderId, AddDishDTO addDishDTO)
        {
            try
            {
                await _orderService.AddDishToOrder(orderId, addDishDTO.DishId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{orderId}/remove-dish/{dishId}")]
        public async Task<IActionResult> RemoveDishFromOrder(int orderId, int dishId)
        {
            try
            {
                await _orderService.RemoveDishFromOrder(orderId, dishId);
                return Ok(new { message = $"Dish {dishId} removed from order {orderId}." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{orderId}/closeAndPay")]
        public async Task<ActionResult<Order>> CloseAndPayOrder(int orderId)
        {
            try
            {
                var order = await _orderService.CloseAndPayOrder(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("queue")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersQueue()
        {
            try
            {
                var orders = await _orderService.GetOrdersQueue();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
