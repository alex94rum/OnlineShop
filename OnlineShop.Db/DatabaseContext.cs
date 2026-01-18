using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!; //DbSet представляет таблицу в базе данных
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Favorite> Favorites { get; set; } = null!;
    public DbSet<Comparison> Comparisons { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<DeliveryUser> DeliveryUsers { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();   // Создаёт базу данных при первом обращении
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryUser>()
            .Property(o => o.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }

}