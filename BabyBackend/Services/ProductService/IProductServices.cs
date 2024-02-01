using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.ProductService
{
    public interface IProductServices
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void AddProduct(ProductDto productDto) ;

    }
}
