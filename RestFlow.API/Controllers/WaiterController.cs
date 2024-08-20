using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

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
            var waiter = await _waiterService.GetById(id);
            if (waiter == null)
            {
                return NotFound();
            }
            return Ok(waiter);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetAll()
        {
            var waiters = await _waiterService.GetAll();
            return Ok(waiters);
        }

        [HttpPost]
        public async Task<ActionResult> Create(WaiterDTO waiter)
        {
            await _waiterService.Add(waiter.FullName, waiter.Password, waiter.ContactInformation);
            return Ok(waiter);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _waiterService.Delete(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Waiter>> Login(WaiterLoginDTO loginDto)
        {
            var waiter = await _waiterService.Login(loginDto.FullName, loginDto.Password);
            if (waiter == null)
            {
                return Unauthorized();
            }
            return Ok(waiter);
        }
    }
}
