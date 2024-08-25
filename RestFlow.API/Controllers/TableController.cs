using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Table id is null");
            }
            var table = await _tableService.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetAll(int restaurantId)
        {
            if (restaurantId == 0)
            {
                return BadRequest("Restaurant id is null");
            }
            var tables = await _tableService.GetAllByRestaurantId(restaurantId);
            if (tables == null)
            {
                return NotFound();
            }
            return Ok(tables);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TableDTO table)
        {
            if (table == null)
            {
                return BadRequest("Table is null");
            }
            await _tableService.Add(table.TableNumber, table.Capacity, table.RestaurantId);
            return Ok(table);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Table id is null");
            }
            await _tableService.Delete(id);
            return NoContent();
        }
    }
}
