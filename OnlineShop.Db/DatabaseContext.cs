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
        //Database.EnsureCreated();   // Создаёт базу данных при первом обращении
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeliveryUser>()
            .Property(o => o.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        modelBuilder.Entity<Cart>()
            .Property(o => o.CreationDateTime)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        modelBuilder.Entity<Product>().HasData(new List<Product>()
        {
            new() { Id = 1, Name = "Продукт 1", Cost = 100, Description = "Описание 1", PhotoPath = "/img/anyProduct.png" },
            new() { Id = 2, Name = "Продукт 2", Cost = 200, Description = "Описание 2", PhotoPath = "/img/anyProduct.png" },
            new() { Id = 3, Name = "Продукт 3", Cost = 300, Description = "Описание 3", PhotoPath = "/img/anyProduct.png" },
        });
    }

}