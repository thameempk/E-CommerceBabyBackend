using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.ProductService
{
    public interface IProductServices
    {
        List<ProductViewDto> GetProducts();
        ProductViewDto GetProductById(int id);
        List<ProductViewDto> GetProductByCategory(int  categoryId);
        //void UploadImage();
        void AddProduct(ProductDto productDto);
        void UpdateProduct(int id ,ProductDto productDto);
        void DeleteProduct(int id);



    }
}
