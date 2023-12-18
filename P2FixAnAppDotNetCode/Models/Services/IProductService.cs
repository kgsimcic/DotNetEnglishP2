using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void UpdateProductQuantities(Cart cart);
    }
}
