using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories;

public class ComparisonsDbRepository : IComparisonsRepository
{
    private readonly DatabaseContext _databaseContext;

    public ComparisonsDbRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Comparison? TryGetByUserId(string userId)
    {
        return _databaseContext.Comparisons.Include(x => x.Items).FirstOrDefault(x => x.UserId == userId);
    }

    public void Add(Product product, string userId)
    {
        var existingComparison = TryGetByUserId(userId);

        if (existingComparison == null)
        {
            existingComparison = new Comparison()
            {
                UserId = userId,
                Items = [product]
            };

            _databaseContext.Comparisons.Add(existingComparison);
        }
        else
        {
            var existingComparisonItem = existingComparison.Items.FirstOrDefault(x => x.Id == product.Id);

            if (existingComparisonItem == null)
            {
                existingComparison.Items.Add(product);
            }
        }

        _databaseContext.SaveChanges();
    }

    public void Delete(int productId, string userId)
    {
        var existingComparison = TryGetByUserId(userId);

        var existingComparisonItem = existingComparison?.Items.FirstOrDefault(x => x.Id == productId);

        if (existingComparisonItem != null)
        {
            existingComparison?.Items.Remove(existingComparisonItem);
        }

        _databaseContext.SaveChanges();
    }

    public void Clear(string userId)
    {
        var existingComparison = TryGetByUserId(userId);

        if (existingComparison != null)
        {
            _databaseContext.Comparisons.Remove(existingComparison);

            _databaseContext.SaveChanges();
        }

    }
}