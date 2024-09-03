using Microsoft.Extensions.Logging;
using RestFlow.Common.Validations;
using RestFlow.DAL.Repositories;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        public async Task<bool> Signup(string userName, string password, int restaurantId)
        {
            try
           {
                _logger.LogInformation("Attempting to sign up user: {UserName}", userName);

                if (!UserValidations.IsValidUserName(userName))
                {
                    _logger.LogWarning("Invalid username format: {UserName}", userName);
                    return false;
                }

                if (!UserValidations.IsValidPassword(password))
                {
                    _logger.LogWarning("Invalid password format for user: {UserName}", userName);
                    return false;
                }

                var result = await _authRepository.Signup(userName, password, restaurantId);

                if (result)
                {
                    _logger.LogInformation("User signed up successfully: {UserName}", userName);
                }
                else
                {
                    _logger.LogWarning("Failed to sign up user: {UserName}", userName);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while signing up user: {UserName}", userName);
                throw; 
            }
        }

        public async Task<bool> Login(string userName, string password, int restaurantId)
        {
            try
            {
                _logger.LogInformation("Attempting to log in user: {UserName}", userName);

                var result = await _authRepository.Login(userName, password, restaurantId);

                if (result)
                {
                    _logger.LogInformation("User logged in successfully: {UserName}", userName);
                }
                else
                {
                    _logger.LogWarning("Failed to log in user: {UserName}", userName);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user: {UserName}", userName);
                throw; 
            }
        }

        public async Task Logout()
        {
            try
            {
                _logger.LogInformation("Attempting to log out user");

                await _authRepository.Logout();

                _logger.LogInformation("User logged out successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging out user");
                throw; 
            }
        }

        
    }
}
