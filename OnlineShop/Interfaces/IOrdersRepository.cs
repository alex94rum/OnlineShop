using OnlineShop.Models;

namespace OnlineShop.Interfaces;

public interface IOrdersRepository
{
    void Add(Order order);
}
