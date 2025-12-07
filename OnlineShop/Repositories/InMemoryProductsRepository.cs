using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public class InMemoryProductsRepository : IProductsRepository
    {
        private int _instanceCounter = 0;

        private readonly List<Product> _products;

        public InMemoryProductsRepository()
        {
            _products =
            [
                new Product(++_instanceCounter, "Товар 1", 1000.0M, "Описание товара 1"),
                new Product(++_instanceCounter, "Товар 2", 2000.0M, "Описание товара 2"),
                new Product(++_instanceCounter, "Товар 3", 3000.0M, "Описание товара 3"),
                new Product(++_instanceCounter, "Товар 4", 4000.0M, "Описание товара 4"),
                new Product(++_instanceCounter, "Товар 5", 5000.0M, "Описание товара 5")
            ];
        }

        public List<Product> GetAll() => _products;

        public Product? TryGetById(int productId) =>
            _products.FirstOrDefault(product => product.Id == productId);
    }
}
