using Azure;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using BabyBackend.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductServices productServices, IWebHostEnvironment webHostEnvironment)
        {
            _productServices = productServices;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]

        public async Task<ActionResult> GetProducts()
        {
            var products = await _productServices.GetProducts();
            
           return Ok(products);

        }




        [HttpGet("paginated-product")]

        public async Task<ActionResult> PaginatedProduct([FromQuery] int pageNumber = 1 , [FromQuery] int PageSize = 10)
        {
            
            return Ok(await _productServices.ProductPagination(pageNumber, PageSize));
        }


        [HttpGet("{id}", Name ="getproducts")]

        public async Task<ActionResult> GetProdectById(int id)
        {
            var products = await _productServices.GetProductById(id);
            return Ok(products);
        }


        [HttpGet("product-by-category")]

        public async Task<ActionResult> GetProductByCategory(int categoryId)
        {
            return Ok(await _productServices.GetProductByCategory(categoryId));
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto , IFormFile image)
        {
            await _productServices.AddProduct(productDto,image);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productServices.DeleteProduct(id);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto, IFormFile image)
        {
            await _productServices.UpdateProduct(id, productDto, image);
            return Ok();
        }

       





    }
}
