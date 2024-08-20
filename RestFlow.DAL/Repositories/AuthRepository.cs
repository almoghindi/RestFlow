using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace RestFlow.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AuthRepository> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<bool> Signup(string userName, string password)
        {
            var user = new IdentityUser { UserName = userName };
            var result = await _userManager.CreateAsync(user, password);
            _logger.LogInformation("Signed up succesfully");
            return result.Succeeded;
        }

        public async Task<bool> Login(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
            _logger.LogInformation("Logged in succesfully");
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Logged out succesfully");

        }
    }
}
