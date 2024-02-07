using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.ProductService
{
    public interface IProductServices
    {
        Task<List<ProductViewDto>> GetProducts();
        Task<ProductViewDto> GetProductById(int id);
        Task<List<ProductViewDto>> GetProductByCategory(int categoryId);
        Task<List<ProductViewDto>> ProductPagination(int pageNumber = 1, int pageSize = 10);
         Task AddProduct(ProductDto productDto, IFormFile image);
        Task UpdateProduct(int id, ProductDto productDto, IFormFile image);
        Task DeleteProduct(int id);



    }
}
