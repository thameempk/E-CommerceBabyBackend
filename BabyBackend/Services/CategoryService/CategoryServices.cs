﻿using BabyBackend.DbContexts;
using BabyBackend.Models.Dto;
using BabyBackend.Models;
using AutoMapper;

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

        public List<CategoryViewDto>  GetCategories()
        {
            var cat = _dbContext.categories.ToList();
            var categories = _mapper.Map<List<CategoryViewDto>>(cat);
            return categories;
          
        }
        public CategoryViewDto GetCategoryById(int id)
        {
            var cat = _dbContext.categories.FirstOrDefault(c => c.Id == id);
            var category = _mapper.Map<CategoryViewDto>(cat);
            return category;
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _dbContext.categories.Add(category);
            _dbContext.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            var cat = _dbContext.categories.FirstOrDefault(c => c.Id == id);    
            _dbContext.categories.Remove(cat);
            _dbContext.SaveChanges();
        }
        public void UpdateCategory(int id, CategoryDto categoryDto)
        {
            var cat = _dbContext.categories.FirstOrDefault(c => c.Id == id);
            cat.Name = categoryDto.Name;
            _dbContext.SaveChanges();
        }
    }
}
