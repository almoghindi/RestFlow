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
            if (orderDTO == null)
            {
                return BadRequest("Order is null");
            }
            var order = await _orderService.InitializeOrder(orderDTO.WaiterId, orderDTO.TableId, orderDTO.RestaurantId);
            if (order == null)
            {
                return BadRequest("Order is null");
            }
            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            if (orderId == 0)
            {
                return BadRequest("Order id is null");
            }

            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return BadRequest("Order is null");
            }
            return Ok(order);
        }


        [HttpPost("{orderId}/addDish")]
        public async Task<ActionResult> AddDishToOrder(int orderId, AddDishDTO addDishDTO)
        {
            if (orderId == 0)
            {
                return BadRequest("Order id is null");
            }
            if (addDishDTO == null)
            {
                return BadRequest("Dish is null");
            }
            await _orderService.AddDishToOrder(orderId, addDishDTO.DishId);
            return NoContent();

        }

        [HttpPost("{orderId}/remove-dish/{dishId}")]
        public async Task<IActionResult> RemoveDishFromOrder(int orderId, int dishId)
        {
            if (orderId == 0)
            {
                return BadRequest("Order id is null");
            }
            if (dishId == 0)
            {
                return BadRequest("Dish id is null");
            }
            await _orderService.RemoveDishFromOrder(orderId, dishId);
            return Ok(new { message = $"Dish {dishId} removed from order {orderId}." });

        }

        [HttpPost("{orderId}/closeAndPay")]
        public async Task<ActionResult<Order>> CloseAndPayOrder(int orderId)
        {
            if (orderId == 0)
            {
                return BadRequest("Order id is null");
            }

            var order = await _orderService.CloseAndPayOrder(orderId);
            if (order == null)
            {
                return BadRequest("Orders is null");
            }
            return Ok(order);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCompletedOrdersByRestaurantId(int restaurantId)
        {
            if (restaurantId == 0)
            {
                return BadRequest("Order id is null");
            }

            var orders = await _orderService.GetCompletedOrdersByRestaurantId(restaurantId);
            if (orders == null)
            {
                return BadRequest("Orders is null");
            }
            return Ok(orders);
        }

        [HttpGet("queue")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersQueue(int restaurantId)
        {
            if (restaurantId == 0)
            {
                return BadRequest("Order id is null");
            }

            var orders = await _orderService.GetOrdersQueue(restaurantId);
            if (orders == null)
            {
                return BadRequest("Orders is null");
            }
            return Ok(orders);
        }
    }

}
