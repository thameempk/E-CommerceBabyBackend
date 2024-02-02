using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.ProductService
{
    public interface IProductServices
    {
        List<ProductViewDto> GetProducts();
        Product GetProductById(int id);
        void AddProduct(ProductDto productDto);
        void UpdateProduct(int id ,ProductDto productDto);
        void DeleteProduct(int id);



    }
}
