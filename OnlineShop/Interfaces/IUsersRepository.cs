using OnlineShop.Models;

namespace OnlineShop.Interfaces;

public interface IUsersRepository
{
    void Add(User user);
    User? TryGetByLogin(string login);
}