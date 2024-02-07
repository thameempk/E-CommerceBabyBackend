using BabyBackend.DbContexts;
using BabyBackend.Models.Dto;
using BabyBackend.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBackend.Services.CategoryService
{
    public class CategoryServices : ICategoryServices
    {
        private readonly BabyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryServices(BabyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewDto>> GetCategories()
        {
            var categories = await _dbContext.categories.ToListAsync();
            return _mapper.Map<List<CategoryViewDto>>(categories);
        }

        public async Task<CategoryViewDto> GetCategoryById(int id)
        {
            var category = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CategoryViewDto>(category);
        }

        public async Task AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _dbContext.categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _dbContext.categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
