using AutoMapper;
using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BabyBackend.Services.ProductService
{
    public class ProductServices : IProductServices
    {
        private readonly BabyDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductServices(BabyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ProductViewDto> GetProducts()
        {
            var products = _dbContext.products.Include(p => p.Category).ToList();
            var productWithCategory = products.Select(p => new ProductViewDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                Category = p.Category.Name
            }).ToList();
            return productWithCategory;
        }

        public Product GetProductById(int id)
        {
            return _dbContext.products.FirstOrDefault(p => p.Id == id);

        }

        public void AddProduct(ProductDto productDto)
        {
            var prd = _mapper.Map<Product>(productDto);
            _dbContext.products.Add(prd);
            _dbContext.SaveChanges();
        }
        public void UpdateProduct(int id, ProductDto productDto)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.Id == id);
            product.ProductName = productDto.ProductName;
            product.ProductDescription = productDto.ProductDescription;
            product.Price = productDto.Price;
            _dbContext.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var prd = _dbContext.products.FirstOrDefault(p => p.Id == id);
            _dbContext.Remove(prd);
            _dbContext.SaveChanges();
        }
    }
}
