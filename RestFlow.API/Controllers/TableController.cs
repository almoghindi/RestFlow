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
            var table = await _tableService.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetAll()
        {
            var tables = await _tableService.GetAll();
            return Ok(tables);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TableDTO table)
        {
            await _tableService.Add(table.TableNumber, table.Capacity);
            return Ok(table);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tableService.Delete(id);
            return NoContent();
        }
    }
}
