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

        public async Task<List<ProductViewDto>> GetProducts()
        {
            var products = await _dbContext.products.Include(p => p.Category).ToListAsync();
            if(products.Count > 0)
            {
                var productWithCategory = products.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    Category = p.Category.Name,
                    ProductImage = p.ProductImage
                }).ToList();
                return productWithCategory;
            }
            return new List<ProductViewDto>();
            
        }

        public async Task<ProductViewDto> GetProductById(int id)
        {
            var products = await _dbContext.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            
            if(products == null)
            {
                ProductViewDto product = new ProductViewDto
                {
                    Id = products.Id,
                    ProductName = products.ProductName,
                    ProductDescription = products.ProductDescription,
                    Price = products.Price,
                    Category = products.Category.Name,
                    ProductImage = products.ProductImage

                };
                return product;
            }
            return new ProductViewDto();

            

        }

        public async Task<List<ProductViewDto>> GetProductByCategory(int categoryId)
        {
            var products = await _dbContext.products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).Select(p => new ProductViewDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                Category = p.Category.Name,
                ProductImage = p.ProductImage
            }).ToListAsync();
            
           
            return products;



        }

        public async Task AddProduct(ProductDto productDto)
        {
            var prd = _mapper.Map<Product>(productDto);
             _dbContext.products.AddAsync(prd);
             await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.Id == id);
            product.ProductName = productDto.ProductName;
            product.ProductDescription = productDto.ProductDescription;
            product.Price = productDto.Price;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var prd = await _dbContext.products.FirstOrDefaultAsync(p => p.Id == id);
            _dbContext.products.Remove(prd);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<ProductViewDto>> ProductPagination(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _dbContext.products.Include(p => p.Category)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();

            var paginatedProducts = products.Select(p => new ProductViewDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                ProductImage = p.ProductImage,
                Category = p.Category.Name
            }).ToList();

            return paginatedProducts;
        }



    }
}
