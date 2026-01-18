using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories;

public class FavoritesDbRepository : IFavoritesRepository
{
    private readonly DatabaseContext _databaseContext;

    public FavoritesDbRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Favorite? TryGetByUserId(string userId)
    {
        return _databaseContext.Favorites.Include(x => x.Items).FirstOrDefault(x => x.UserId == userId);
    }

    public void Add(Product product, string userId)
    {
        var existingFavorite = TryGetByUserId(userId);

        if (existingFavorite == null)
        {
            existingFavorite = new Favorite()
            {
                UserId = userId,
                Items = [product]
            };

            _databaseContext.Favorites.Add(existingFavorite);
        }
        else
        {
            var existingFavoriteItem = existingFavorite.Items.FirstOrDefault(x => x.Id == product.Id);

            if (existingFavoriteItem == null)
            {
                existingFavorite.Items.Add(product);
            }
        }

        _databaseContext.SaveChanges();
    }

    public void Delete(int productId, string userId)
    {
        var existingFavorite = TryGetByUserId(userId);

        var existingFavoriteItem = existingFavorite?.Items.FirstOrDefault(x => x.Id == productId);

        if (existingFavoriteItem != null)
        {
            existingFavorite?.Items.Remove(existingFavoriteItem);
        }

        _databaseContext.SaveChanges();
    }

    public void Clear(string userId)
    {
        var existingFavorite = TryGetByUserId(userId);

        if (existingFavorite != null)
        {
            _databaseContext.Favorites.Remove(existingFavorite);

            _databaseContext.SaveChanges();
        }
    }
}