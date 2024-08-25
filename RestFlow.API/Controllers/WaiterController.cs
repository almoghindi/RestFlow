using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;
using static StackExchange.Redis.Role;

namespace RestFlow.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WaiterController : ControllerBase
    {
        private readonly IWaiterService _waiterService;

        public WaiterController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Waiter id is null");
            }
            var waiter = await _waiterService.GetById(id);
            if (waiter == null)
            {
                return NotFound();
            }
            return Ok(waiter);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetAll(int restaurantId)
        {
            if (restaurantId == 0)
            {
                return BadRequest("Restaurant id is null");
            }
            var waiters = await _waiterService.GetAllByRestaurantId(restaurantId);
            if (waiters == null)
            {
                return NotFound();
            }
            return Ok(waiters);
        }

        [HttpPost]
        public async Task<ActionResult> Create(WaiterDTO waiter)
        {
            if (waiter == null)
            {
                return BadRequest("Waiter is null");
            }
            await _waiterService.Add(waiter.FullName, waiter.Password, waiter.ContactInformation, waiter.RestaurantId);
            return Ok(waiter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Waiter id is null");
            }
            await _waiterService.Delete(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Waiter>> Login(WaiterLoginDTO loginDto)
        {
            if(loginDto == null)
            {
                return BadRequest("Invalid Credentials");
            }
            var waiter = await _waiterService.Login(loginDto.FullName, loginDto.Password, loginDto.RestaurantId);
            if (waiter == null)
            {
                return Unauthorized();
            }
            return Ok(waiter);
        }
    }
}
