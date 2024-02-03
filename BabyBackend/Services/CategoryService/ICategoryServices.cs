using BabyBackend.Models;
using BabyBackend.Models.Dto;

namespace BabyBackend.Services.CategoryService
{
    public interface ICategoryServices
    {
        List<CategoryViewDto> GetCategories();
        CategoryViewDto GetCategoryById(int id);

        void AddCategory(CategoryDto categoryDto);
        void DeleteCategory(int id);
        void UpdateCategory(int id, CategoryDto categoryDto);
    }
}
