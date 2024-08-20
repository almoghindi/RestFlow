using Microsoft.Extensions.Logging;
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

        public async Task<bool> Signup(string userName, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to sign up user: {UserName}", userName);

                if (!IsValidUserName(userName))
                {
                    _logger.LogWarning("Invalid username format: {UserName}", userName);
                    return false;
                }

                if (!IsValidPassword(password))
                {
                    _logger.LogWarning("Invalid password format for user: {UserName}", userName);
                    return false;
                }

                var result = await _authRepository.Signup(userName, password);

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

        public async Task<bool> Login(string userName, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to log in user: {UserName}", userName);

                var result = await _authRepository.Login(userName, password);

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

        private bool IsValidUserName(string userName)
        {
            return Regex.IsMatch(userName, @"^[a-zA-Z0-9]{3,20}$");
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 && Regex.IsMatch(password, @"\d");
        }
    }
}
