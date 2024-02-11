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
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly string HostUrl;
        public ProductServices(BabyDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            HostUrl = _configuration["HostUrl:url"];
        }

        public async Task<List<ProductViewDto>> GetProducts()
        {

            var products = await _dbContext.products.Include(p => p.Category).ToListAsync();
            if (products.Count > 0)
            {
                var productWithCategory = products.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    Category = p.Category.Name,
                    ProductImage = HostUrl + p.ProductImage
                }).ToList();
                return productWithCategory;
            }
            return new List<ProductViewDto>();

        }




        public async Task<ProductViewDto> GetProductById(int id)
        {

            var products = await _dbContext.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (products != null)
            {
                ProductViewDto product = new ProductViewDto
                {
                    Id = products.Id,
                    ProductName = products.ProductName,
                    ProductDescription = products.ProductDescription,
                    Price = products.Price,
                    Category = products.Category.Name,
                    ProductImage = HostUrl + products.ProductImage

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
            if (products != null)
            {
                return products;
            }
            return new List<ProductViewDto>();


        }

        public async Task AddProduct(ProductDto productDto, IFormFile image)
        {
            try
            {
                string productImage = null;
            

                if (image != null && image.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
            
                    productImage = "/Uploads/Product/" + fileName;
                }
                else
                {
                    productImage =  "/Uploads/common/noimage.png";
                }


                var prd = _mapper.Map<Product>(productDto);

                prd.ProductImage = productImage;



                _dbContext.products.AddAsync(prd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error adding product: " + ex.Message, ex);
            }
        }
        public async Task UpdateProduct(int id, ProductDto productDto, IFormFile image)
        {
            try
            {
                var product = _dbContext.products.FirstOrDefault(p => p.Id == id);

                if (product != null)
                {
                    product.ProductName = productDto.ProductName;
                    product.ProductDescription = productDto.ProductDescription;
                    product.Price = productDto.Price;


                    if (image != null && image.Length > 0)
                    {

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Product", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        product.ProductImage =  "/Uploads/Product/" + fileName;
                    }
                    else
                    {
                        product.ProductImage =  "/Uploads/common/noimage.png";
                    }


                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Product with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error updating product with ID {id}: {ex.Message}", ex);
            }
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

