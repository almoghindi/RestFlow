using Microsoft.AspNetCore.Mvc;
using RestFlow.API.DTO;
using RestFlow.BL.Services;

namespace RestFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User is null");
            }
            if (string.IsNullOrEmpty(userDTO.Name) || string.IsNullOrEmpty(userDTO.Password) || userDTO.RestaurantId == 0)
            {
                return BadRequest("Invalid credentials");
            }
            var result = await _authService.Signup(userDTO.Name, userDTO.Password, userDTO.RestaurantId);
            if (!result)
            {
                return BadRequest("Signup failed.");
            }
            return Ok("User signed up successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User is null");
            }
            if (string.IsNullOrEmpty(userDTO.Name) || string.IsNullOrEmpty(userDTO.Password) || userDTO.RestaurantId == 0)
            {
                return BadRequest("Invalid credentials");
            }
            var result = await _authService.Login(userDTO.Name, userDTO.Password, userDTO.RestaurantId);
            if (!result)
            {
                return Unauthorized("Login failed.");
            }
            return Ok("User logged in successfully.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok("User logged out successfully.");
        }
    }
}
