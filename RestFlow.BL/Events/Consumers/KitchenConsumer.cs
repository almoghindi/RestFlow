using RestFlow.BL.Services;
using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Observer.Consumers
{
    public class KitchenObserver : IOrderObserver
    {
        private readonly IOrderService _orderService;

        public KitchenObserver(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void Update(Order order)
        {
            Console.WriteLine($"KitchenObserver: Order {order.OrderId} updated.");
        }

        public IEnumerable<Order> ProcessQueue(int restaurantId)
        {
            Console.WriteLine("KitchenObserver: Processing orders queue...");

            var ordersQueue = _orderService.GetOrdersQueue(restaurantId).Result;

            foreach (var order in ordersQueue)
            {
                Console.WriteLine($"Processing Order {order.OrderId} for Table {order.TableId}.");
            }

            return ordersQueue;
        }
    }
}
