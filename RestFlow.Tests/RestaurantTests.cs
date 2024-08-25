using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.Tests
{
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantService> _mockRestaurantService;
        private readonly RestaurantController _controller;

        public RestaurantControllerTests()
        {
            _mockRestaurantService = new Mock<IRestaurantService>();
            _controller = new RestaurantController(_mockRestaurantService.Object);
        }

        [Fact]
        public async Task GetById_RestaurantExists_ReturnsOkResult()
        {
            var restaurantId = 1;
            var restaurant = new Restaurant { Id = restaurantId, Name = "Test Restaurant", Password = "encrypted_password" };
            _mockRestaurantService.Setup(service => service.GetById(restaurantId))
                                  .ReturnsAsync(restaurant);

            var result = await _controller.GetById(restaurantId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRestaurant = Assert.IsType<Restaurant>(okResult.Value);
            Assert.Equal(restaurantId, returnedRestaurant.Id);
        }

        [Fact]
        public async Task GetById_RestaurantDoesNotExist_ReturnsNotFound()
        {
            var restaurantId = 1;
            _mockRestaurantService.Setup(service => service.GetById(restaurantId))
                                  .ReturnsAsync((Restaurant)null);

            var result = await _controller.GetById(restaurantId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ValidRestaurant_ReturnsOkResult()
        {
            var restaurant =  new Restaurant { Id = 1, Name = "Test Restaurant", Password = "encrypted_password" };
            var restaurantDto = new RestaurantDTO { Name = "Test Restaurant", Password = "encrypted_password" };
            _mockRestaurantService.Setup(service => service.Create(restaurantDto.Name, restaurantDto.Password))
                                  .ReturnsAsync(restaurant);

            var result = await _controller.Create(restaurantDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdRestaurant = Assert.IsType<Restaurant>(okResult.Value);
            Assert.Equal(restaurant.Id, createdRestaurant.Id);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResultWithIdAndName()
        {
            var loginRequest = new RestaurantDTO { Name = "Test Restaurant", Password = "password" };
            var restaurant = new Restaurant { Id = 1, Name = "Test Restaurant", Password = "encrypted_password" };

            _mockRestaurantService.Setup(service => service.Login(loginRequest.Name, loginRequest.Password))
                                  .ReturnsAsync(restaurant);

            var result = await _controller.Login(loginRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnedObject = okResult.Value;
            Assert.NotNull(returnedObject);

            var idProperty = returnedObject.GetType().GetProperty("Id");
            var nameProperty = returnedObject.GetType().GetProperty("Name");

            Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);

            var returnedId = (int)idProperty.GetValue(returnedObject);
            var returnedName = (string)nameProperty.GetValue(returnedObject);

            Assert.Equal(restaurant.Id, returnedId);
            Assert.Equal(restaurant.Name, returnedName);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var loginRequest = new RestaurantDTO { Name = "Test Restaurant", Password = "wrong_password" };
            _mockRestaurantService.Setup(service => service.Login(loginRequest.Name, loginRequest.Password))
                                  .ReturnsAsync((Restaurant)null);

            var result = await _controller.Login(loginRequest);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Delete_RestaurantExists_ReturnsNoContent()
        {
            var restaurantId = 1;
            _mockRestaurantService.Setup(service => service.Delete(restaurantId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(restaurantId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_RestaurantDoesNotExist_ReturnsNotFound()
        {
            var restaurantId = 1;
            _mockRestaurantService.Setup(service => service.Delete(restaurantId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(restaurantId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
