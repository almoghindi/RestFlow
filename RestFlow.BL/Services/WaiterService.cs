using Microsoft.Extensions.Logging;
using RestFlow.BL.Factory;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Services
{
    public class WaiterService : IWaiterService
    {
        private readonly IWaiterRepository _waiterRepository;
        private readonly IModelFactory _modelFactory;
        private readonly ILogger<WaiterService> _logger;

        public WaiterService(IWaiterRepository waiterRepository, IModelFactory modelFactory , ILogger<WaiterService> logger)
        {
            _waiterRepository = waiterRepository;
            _modelFactory = modelFactory;
            _logger = logger;
        }

        public async Task<Waiter> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch Waiter with ID: {id}");
                var waiter = await _waiterRepository.GetById(id);
                if (waiter == null)
                {
                    _logger.LogWarning($"Waiter with ID: {id} not found");
                }
                return waiter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching Waiter with ID: {id}");
                throw; // Rethrow the exception to maintain stack trace
            }
        }

        public async Task<IEnumerable<Waiter>> GetAll()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch all Waiters");
                var waiters = await _waiterRepository.GetAll();
                return waiters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all Waiters");
                throw;
            }
        }

        public async Task Add(string fullname, string password, string contactInformation)
        {
            try
            {
                _logger.LogInformation($"Attempting to add Waiter: {fullname}");
                Waiter waiter = _modelFactory.CreateWaiter(fullname, password, contactInformation);
                await _waiterRepository.Add(waiter);
                _logger.LogInformation($"Successfully added Waiter with ID: {waiter.WaiterId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding Waiter: {fullname}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete Waiter with ID: {id}");
                await _waiterRepository.Delete(id);
                _logger.LogInformation($"Successfully deleted Waiter with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting Waiter with ID: {id}");
                throw;
            }
        }

        public async Task<Waiter> Login(string fullName, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to login Waiter");
                IEnumerable<Waiter> waiters = await _waiterRepository.GetAll();
                var waiter = waiters.FirstOrDefault(w => w.FullName == fullName && w.Password == password);
                if (waiter == null)
                {
                    _logger.LogWarning("Invalid login attempt");
                }
                return waiter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Waiter login attempt");
                throw;
            }
        }
    }
}
