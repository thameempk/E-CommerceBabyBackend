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

        public ProductViewDto GetProductById(int id)
        {
            var products = _dbContext.products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            ProductViewDto product = new ProductViewDto
            {
                Id = products.Id,
                ProductName = products.ProductName,
                ProductDescription = products.ProductDescription,
                Price = products.Price,
                Category = products.Category.Name

            };
            return product;

        }

        public List<ProductViewDto> GetProductByCategory(int categoryId)
        {
            var products = _dbContext.products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).Select(p => new ProductViewDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                Category = p.Category.Name
            }).ToList();
            
           
            return products;



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
