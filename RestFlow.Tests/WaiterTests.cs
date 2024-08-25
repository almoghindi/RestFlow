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
    public class WaiterTests
    {
        private readonly WaiterController _controller;
        private readonly Mock<IWaiterService> _mockWaiterService;

        public WaiterTests()
        {
            _mockWaiterService = new Mock<IWaiterService>();
            _controller = new WaiterController(_mockWaiterService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WhenWaiterExists()
        {
            var waiterId = 1;
            var waiter = new Waiter
            {
                WaiterId = waiterId,
                FullName = "John Doe",
                ContactInformation = "123-456-7890",
                RestaurantId = 1
            };

            _mockWaiterService.Setup(service => service.GetById(waiterId))
                .ReturnsAsync(waiter);

            var result = await _controller.Get(waiterId) as ActionResult<Waiter>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(waiter);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenWaiterDoesNotExist()
        {
            var waiterId = 1;

            _mockWaiterService.Setup(service => service.GetById(waiterId))
                .ReturnsAsync((Waiter)null);

            var result = await _controller.Get(waiterId) as ActionResult<Waiter>;

            result.Result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WhenWaitersExist()
        {
            var waiters = new List<Waiter>
            {
                new Waiter { WaiterId = 1, FullName = "John Doe", ContactInformation = "123-456-7890", RestaurantId = 1 },
                new Waiter { WaiterId = 2, FullName = "Jane Smith", ContactInformation = "987-654-3210", RestaurantId = 1 }
            };

            _mockWaiterService.Setup(service => service.GetAllByRestaurantId(1))
                .ReturnsAsync(waiters);

            var result = await _controller.GetAll(1) as ActionResult<IEnumerable<Waiter>>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(waiters);
        }

        [Fact]
        public async Task Create_ShouldReturnOkResult_WhenWaiterIsCreated()
        {
            var waiterDto = new WaiterDTO
            {
                FullName = "John Doe",
                Password = "password",
                ContactInformation = "123-456-7890",
                RestaurantId = 1
            };

            var result = await _controller.Create(waiterDto) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(waiterDto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenWaiterIsDeleted()
        {
            var waiterId = 1;

            _mockWaiterService.Setup(service => service.Delete(waiterId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(waiterId) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResult_WhenCredentialsAreValid()
        {
            var loginDto = new WaiterLoginDTO
            {
                FullName = "John Doe",
                Password = "password",
                RestaurantId = 1
            };

            var waiter = new Waiter
            {
                WaiterId = 1,
                FullName = "John Doe",
                ContactInformation = "123-456-7890",
                RestaurantId = 1
            };

            _mockWaiterService.Setup(service => service.Login(loginDto.FullName, loginDto.Password, loginDto.RestaurantId))
                .ReturnsAsync(waiter);

            var result = await _controller.Login(loginDto) as ActionResult<Waiter>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(waiter);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            var loginDto = new WaiterLoginDTO
            {
                FullName = "John Doe",
                Password = "wrongpassword",
                RestaurantId = 1
            };

            _mockWaiterService.Setup(service => service.Login(loginDto.FullName, loginDto.Password, loginDto.RestaurantId))
                .ReturnsAsync((Waiter)null);

            var result = await _controller.Login(loginDto) as ActionResult<Waiter>;

            result.Result.Should().BeOfType<UnauthorizedResult>();
            var unauthorizedResult = result.Result as UnauthorizedResult;
            unauthorizedResult.StatusCode.Should().Be(401);
        }
    }
}
