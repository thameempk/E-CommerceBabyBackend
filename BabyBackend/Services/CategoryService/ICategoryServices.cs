using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.CategoryService
{
    public interface ICategoryServices
    {
        Task<List<CategoryViewDto>> GetCategories();
        Task<CategoryViewDto> GetCategoryById(int id);

        Task AddCategory(CategoryDto categoryDto);
        Task DeleteCategory(int id);
        Task UpdateCategory(int id, CategoryDto categoryDto);
    }
}
