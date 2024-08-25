using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestFlow.Tests
{
    public class OrderControllerTests
    {
        private readonly OrderController _controller;
        private readonly Mock<IOrderService> _mockOrderService;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }

        [Fact]
        public async Task InitializeOrder_ShouldReturnOkResult_WhenOrderIsInitialized()
        {
            var orderDTO = new OrderDTO
            {
                WaiterId = 1,
                TableId = 1,
                RestaurantId = 1,
            };

            var order = new Order
            {
                OrderId = 1,
                WaiterId = orderDTO.WaiterId,
                TableId = orderDTO.TableId,
                RestaurantId = orderDTO.RestaurantId,
            };

            _mockOrderService.Setup(service => service.InitializeOrder(orderDTO.WaiterId, orderDTO.TableId, orderDTO.RestaurantId))
                .ReturnsAsync(order);

            var result = await _controller.InitializeOrder(orderDTO) as ActionResult<Order>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task InitializeOrder_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var orderDTO = new OrderDTO
            {
                WaiterId = 1,
                TableId = 1,
                RestaurantId = 1,
            };

            _mockOrderService.Setup(service => service.InitializeOrder(orderDTO.WaiterId, orderDTO.TableId, orderDTO.RestaurantId))
                .ThrowsAsync(new Exception("Initialization error"));

            await Assert.ThrowsAsync<Exception>(async () => await _controller.InitializeOrder(orderDTO));

        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOkResult_WhenOrderExists()
        {
            var orderId = 1;
            var order = new Order
            {
                OrderId = orderId,
                WaiterId = 1,
                TableId = 1,
                RestaurantId = 1,
            };

            _mockOrderService.Setup(service => service.GetOrderById(orderId))
                .ReturnsAsync(order);

            var result = await _controller.GetOrderById(orderId) as IActionResult;

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var orderId = 1;

            _mockOrderService.Setup(service => service.GetOrderById(orderId))
                .ThrowsAsync(new Exception("Order is null"));

            await Assert.ThrowsAsync<Exception>(async () => await _controller.GetOrderById(orderId));
        }

        [Fact]
        public async Task AddDishToOrder_ShouldReturnNoContent_WhenDishIsAdded()
        {
            var orderId = 1;
            var addDishDTO = new AddDishDTO
            {
                DishId = 1
            };

            _mockOrderService.Setup(service => service.AddDishToOrder(orderId, addDishDTO.DishId))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddDishToOrder(orderId, addDishDTO) as ActionResult;

            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task AddDishToOrder_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var orderId = 1;
            var addDishDTO = new AddDishDTO
            {
                DishId = 1
            };

            _mockOrderService.Setup(service => service.AddDishToOrder(orderId, addDishDTO.DishId))
                .ThrowsAsync(new Exception("Add dish error"));

            await Assert.ThrowsAsync<Exception>(async () => await _controller.AddDishToOrder(orderId, addDishDTO));
        }

        [Fact]
        public async Task RemoveDishFromOrder_ShouldReturnOkResult_WhenDishIsRemoved()
        {
            var orderId = 1;
            var dishId = 1;

            _mockOrderService.Setup(service => service.RemoveDishFromOrder(orderId, dishId))
                .Returns(Task.CompletedTask);

            var result = await _controller.RemoveDishFromOrder(orderId, dishId) as IActionResult;

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { message = $"Dish {dishId} removed from order {orderId}." });
        }

        [Fact]
        public async Task RemoveDishFromOrder_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var orderId = 1;
            var dishId = 1;

            _mockOrderService.Setup(service => service.RemoveDishFromOrder(orderId, dishId))
                .ThrowsAsync(new Exception("Remove dish error"));

            await Assert.ThrowsAsync<Exception>(async () => await _controller.RemoveDishFromOrder(orderId, dishId));
        }

        [Fact]
        public async Task CloseAndPayOrder_ShouldReturnOkResult_WhenOrderIsClosedAndPaid()
        {
            var orderId = 1;
            var order = new Order
            {
                OrderId = orderId,
                WaiterId = 1,
                TableId = 1
            };

            _mockOrderService.Setup(service => service.CloseAndPayOrder(orderId))
                .ReturnsAsync(order);

            var result = await _controller.CloseAndPayOrder(orderId) as ActionResult<Order>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task CloseAndPayOrder_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var orderId = 1;

            _mockOrderService.Setup(service => service.CloseAndPayOrder(orderId))
                .ThrowsAsync(new Exception("Close and pay error"));

            await Assert.ThrowsAsync<Exception>(async () => await _controller.CloseAndPayOrder(orderId));
        }

        [Fact]
        public async Task GetOrdersQueue_ShouldReturnOkResult_WhenOrdersAreAvailable()
        {
            var orders = new List<Order>
            {
                new Order { OrderId = 1, WaiterId = 1, TableId = 1, RestaurantId = 1 },
                new Order { OrderId = 2, WaiterId = 2, TableId = 2, RestaurantId = 1 }
            };

            _mockOrderService.Setup(service => service.GetOrdersQueue(1))
                .ReturnsAsync(orders);

            var result = await _controller.GetOrdersQueue(1) as ActionResult<IEnumerable<Order>>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(orders);
        }

        [Fact]
        public async Task GetOrdersQueue_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            _mockOrderService.Setup(service => service.GetOrdersQueue(1))
                .ThrowsAsync(new Exception("Get orders queue error"));

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _controller.GetOrdersQueue(1));
            exception.Message.Should().Be("Get orders queue error");
        }
    }
}
