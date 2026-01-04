using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Repositories;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly List<User> _users = [];

    public void Add(User user)
    {
        user.Id = Guid.NewGuid();

        _users.Add(user);
    }


    public User? TryGetByLogin(string login) =>
        _users.FirstOrDefault(user => user.Login == login);
}