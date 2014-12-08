using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Model;

namespace Sample.Logic
{
    public interface IRepository
    {
        Task LoadSomeDataIntoMemoryAsync();

        Task<List<Product>> GetProductsAsync();

        Product CreateNewProduct();
        void Refresh();
        void AddProduct(Product product);
    }

    public class DemoRepository : IRepository
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product {Name = "Product 1"},
            new Product {Name = "Product 2"},
            new Product {Name = "Product 3"},
        };

        public Task LoadSomeDataIntoMemoryAsync()
        {
            return Task.Delay(10);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return Task.FromResult(_products);
        }

        public Product CreateNewProduct()
        {
            return new Product();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }
    }
}
