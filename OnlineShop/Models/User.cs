using OnlineShop.Areas.Admin.Models;

namespace OnlineShop.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public Role? Role { get; set; }
}