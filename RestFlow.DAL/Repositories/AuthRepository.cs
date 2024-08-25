using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthRepository> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<bool> Signup(string userName, string password, int restaurantId)
        {
            var user = new ApplicationUser { UserName = userName, RestaurantId = restaurantId };
            var result = await _userManager.CreateAsync(user, password);
            _logger.LogInformation("Signed up succesfully");
            return result.Succeeded;
        }

        public async Task<bool> Login(string userName, string password, int restaurantId)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || user.RestaurantId != restaurantId)
            {
                _logger.LogWarning("Login failed: invalid username or restaurant ID");
                return false;
            }

            // Check the password
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("Logged in successfully with RestaurantId: {RestaurantId}", restaurantId);
            }
            else
            {
                _logger.LogWarning("Login failed: invalid password");
            }

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Logged out succesfully");

        }
    }
}
