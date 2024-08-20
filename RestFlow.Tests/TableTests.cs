using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestFlow.Tests
{
    public class TableTests
    {
        private readonly TableController _controller;
        private readonly Mock<ITableService> _mockTableService;

        public TableTests()
        {
            _mockTableService = new Mock<ITableService>();
            _controller = new TableController(_mockTableService.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WhenTableExists()
        {
            var tableId = 1;
            var table = new Table
            {
                TableId = tableId,
                TableNumber = "5",
                Capacity = 4
            };

            _mockTableService.Setup(service => service.GetById(tableId))
                .ReturnsAsync(table);

            var result = await _controller.GetById(tableId) as ActionResult<Table>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(table);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenTableDoesNotExist()
        {
            var tableId = 1;

            _mockTableService.Setup(service => service.GetById(tableId))
                .ReturnsAsync((Table)null);

            var result = await _controller.GetById(tableId) as ActionResult<Table>;

            result.Result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WhenTablesExist()
        {
            var tables = new List<Table>
            {
                new Table { TableId = 1, TableNumber = "5", Capacity = 4 },
                new Table { TableId = 2, TableNumber = "10", Capacity = 2 }
            };

            _mockTableService.Setup(service => service.GetAll())
                .ReturnsAsync(tables);

            var result = await _controller.GetAll() as ActionResult<IEnumerable<Table>>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(tables);
        }

        [Fact]
        public async Task Create_ShouldReturnOkResult_WhenTableIsCreated()
        {
            var tableDto = new TableDTO
            {
                TableNumber = "5",
                Capacity = 4
            };

            var result = await _controller.Create(tableDto) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(tableDto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenTableIsDeleted()
        {
            var tableId = 1;

            _mockTableService.Setup(service => service.Delete(tableId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(tableId) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }
    }
}
