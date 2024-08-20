using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;

namespace RestFlow.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(DataDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> InitializeOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(o => o.Dishes).FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddDishToOrder(int orderId, Dish dish)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) throw new ArgumentException("Order not found");

            order.AddDish(dish);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDishFromOrder(int orderId, int dishId)
        {
            var order = await _context.Orders.Include(o => o.Dishes).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null)
            {
                var dishToRemove = order.Dishes.FirstOrDefault(d => d.DishId == dishId);
                if (dishToRemove != null)
                {
                    order.Dishes.Remove(dishToRemove);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Order>> GetIncompleteOrders()
        {
            return await _context.Orders
                           .Include(o => o.Dishes)
                           .Include(o => o.Table)
                           .Include(o => o.Waiter)
                           .Where(o => !o.IsCompleted)
                           .ToListAsync();
        }

        public async Task<Order> CloseAndPayOrder(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Dishes).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null) throw new ArgumentException("Order not found");

            order.TotalAmount = order.Dishes.Sum(d => d.Price);
            order.CloseOrder();
            await _context.SaveChangesAsync();
            return order;

        }

        public async Task<IEnumerable<Order>> GetOrdersQueue()
        {
            return await _context.Orders.Where(o => !o.IsCompleted).ToListAsync();
        }


    }
}
