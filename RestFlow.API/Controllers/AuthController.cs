using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Signup(string userName, string password)
        {
            var result = await _authService.Signup(userName, password);
            if (result)
            {
                return Ok("User signed up successfully.");
            }
            return BadRequest("Signup failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var result = await _authService.Login(userName, password);
            if (result)
            {
                return Ok("User logged in successfully.");
            }
            return Unauthorized("Login failed.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok("User logged out successfully.");
        }
    }
}
