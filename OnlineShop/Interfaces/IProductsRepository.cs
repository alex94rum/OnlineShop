using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product? TryGetById(int productId);
        void Add(Product product);
        void Delete(int productId);
        void Update(Product product);
    }
}
