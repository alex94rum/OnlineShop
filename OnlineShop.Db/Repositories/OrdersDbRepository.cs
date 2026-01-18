using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories;

public class OrdersDbRepository : IOrdersRepository
{
    private readonly DatabaseContext _databaseContext;

    public OrdersDbRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Add(Order order)
    {
        order.Id = Guid.NewGuid();
        order.CreationDateTime = DateTime.UtcNow;
        order.DeliveryUser.Id = Guid.NewGuid();
        order.Status = OrderStatus.Created;

        _databaseContext.Orders.Add(order);

        _databaseContext.SaveChanges();
    }

    public List<Order> GetAll() => _databaseContext.Orders.Include(x => x.DeliveryUser).Include(x => x.Items).ThenInclude(x => x.Product).ToList();

    public Order? TryGetById(Guid orderId) =>
        _databaseContext.Orders.Include(x => x.DeliveryUser).Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(order => order.Id == orderId);

    public void UpdateStatus(Guid orderId, OrderStatus newStatus)
    {
        var existingOrder = TryGetById(orderId);

        if (existingOrder != null)
        {
            existingOrder.Status = newStatus;

            _databaseContext.SaveChanges();
        }
    }

}