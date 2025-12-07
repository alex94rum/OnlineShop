using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class InMemoryOrdersRepository : IOrdersRepository
    {
        private readonly List<Order> _orders = [];

        public void Add(Order order)
        {
            order.Id = Guid.NewGuid();

            _orders.Add(order);
        }
    }
}
