using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        Cart? TryGetByUserId(string userId);
        void Subtract(int productId, string userId);
        void Clear(string userId);
    }
}
