using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Signup_ShouldReturnOk_WhenSignupSucceeds()
        {
            UserDTO userDTO = new UserDTO { Name="testuser", Password="testpassword", RestaurantId = 1 };
            _mockAuthService.Setup(service => service.Signup(userDTO.Name, userDTO.Password, userDTO.RestaurantId))
                .ReturnsAsync(true);

            var result = await _controller.Signup(userDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User signed up successfully.", okResult.Value);
        }

        [Fact]
        public async Task Signup_ShouldReturnBadRequest_WhenSignupFails()
        {
            UserDTO userDTO = new UserDTO { Name = "testuser", Password = "testpassword", RestaurantId = 1 };
            _mockAuthService.Setup(service => service.Signup(userDTO.Name, userDTO.Password, userDTO.RestaurantId))
                .ReturnsAsync(false);

            var result = await _controller.Signup(userDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Signup failed.", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginSucceeds()
        {
            UserDTO userDTO = new UserDTO { Name = "testuser", Password = "testpassword", RestaurantId = 1 };

            _mockAuthService.Setup(service => service.Login(userDTO.Name, userDTO.Password, userDTO.RestaurantId))
                .ReturnsAsync(true);

            var result = await _controller.Login(userDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User logged in successfully.", okResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenLoginFails()
        {
            UserDTO userDTO = new UserDTO { Name = "testuser", Password = "testpassword", RestaurantId = 1 };

            _mockAuthService.Setup(service => service.Login(userDTO.Name, userDTO.Password, userDTO.RestaurantId))
                .ReturnsAsync(false);

            var result = await _controller.Login(userDTO);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Login failed.", unauthorizedResult.Value);
        }

        [Fact]
        public async Task Logout_ShouldReturnOk()
        {
            var result = await _controller.Logout();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User logged out successfully.", okResult.Value);
        }
    }
}
