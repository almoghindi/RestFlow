using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Restaurant id is null");
            }
            var restaurant = await _restaurantService.GetById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantDTO restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest("Restaurant is null");
            }
            var createdRestaurant = await _restaurantService.Create(restaurant.Name, restaurant.Password);
            if (createdRestaurant == null)
            {
                return NotFound();
            }
            return Ok(createdRestaurant);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RestaurantDTO loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Login Request is null");
            }
            var restaurant = await _restaurantService.Login(loginRequest.Name, loginRequest.Password);
            if (restaurant == null)
            {
                return Unauthorized();
            }
            return Ok(new {restaurant.Id, restaurant.Name});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Restaurant id is null");
            }
            await _restaurantService.Delete(id);
            return NoContent();
        }
    }
}
