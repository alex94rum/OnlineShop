using OnlineShop.Areas.Admin.Models;
using OnlineShop.Interfaces;

namespace OnlineShop.Repositories;

public class InMemoryRolesRepository : IRolesRepository
{
    private readonly List<Role> _roles = [];

    public InMemoryRolesRepository()
    {
        _roles =
        [
            new Role(){Id = Guid.NewGuid(), Name = "Admin"},
            new Role(){Id = Guid.NewGuid(), Name = "Moderator"},
            new Role(){Id = Guid.NewGuid(), Name = "User"},
            new Role(){Id = Guid.NewGuid(), Name = "Developer"},
            new Role(){Id = Guid.NewGuid(), Name = "Guest"},
        ];
    }

    public void Add(Role role)
    {
        role.Id = Guid.NewGuid();

        _roles.Add(role);
    }

    public void Delete(Guid roleId)
    {
        var existingRole = TryGetById(roleId);

        if (existingRole != null)
        {
            _roles.Remove(existingRole);
        }
    }

    public List<Role> GetAll() => _roles;


    public Role? TryGetById(Guid roleId) =>
        _roles.FirstOrDefault(role => role.Id == roleId);

    public Role? TryGetByName(string roleName) =>
        _roles.FirstOrDefault(role => role.Name == roleName);
}