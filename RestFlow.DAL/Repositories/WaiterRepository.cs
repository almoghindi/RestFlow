using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly DataDbContext _context;
        private readonly ILogger<WaiterRepository> _logger;

        public WaiterRepository(DataDbContext context, ILogger<WaiterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Waiter> GetById(int id)
        {
            _logger.LogInformation($"Fetching Waiter with ID: {id}");
            var waiter = await _context.Waiters.FindAsync(id);
            if (waiter == null)
            {
                _logger.LogWarning($"Waiter with ID: {id} not found");
            }
            return waiter;
        }

        public async Task<IEnumerable<Waiter>> GetAllByRestaurantId(int restaurantId)
        {
            _logger.LogInformation("Fetching all Waiters");
            var waiters = await _context.Waiters.Where(w => w.RestaurantId == restaurantId).ToListAsync();
            return waiters;
        }

        public async Task Add(Waiter waiter)
        {
            _logger.LogInformation($"Adding Waiter with ID: {waiter.WaiterId}");
            _context.Waiters.AddAsync(waiter);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Waiter with ID: {waiter.WaiterId} added successfully");
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleting Waiter with ID: {id}");
            var waiter = await _context.Waiters.FindAsync(id);
            if (waiter != null)
            {
                _context.Waiters.Remove(waiter);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Waiter with ID: {id} deleted successfully");
            }
            else
            {
                _logger.LogWarning($"Waiter with ID: {id} not found for deletion");
            }
        }

        public async Task<Waiter> Login(string fullName, string password, int restaurantId)
        {
            _logger.LogInformation($"Logging Waiter with name: {fullName}");
            var waiter = _context.Waiters.FirstOrDefault(w => w.FullName == fullName && w.Password == password && w.RestaurantId == restaurantId);
            _logger.LogInformation($"Waiter with ID: {waiter.WaiterId} logged in succesfully");
            return waiter;
        }

    }

}
