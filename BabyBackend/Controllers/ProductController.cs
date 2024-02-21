using Azure;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using BabyBackend.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var products = await _productServices.GetProducts();

                return Ok(products);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            

        }




        [HttpGet("paginated-product")]
 
        public async Task<ActionResult> PaginatedProduct([FromQuery] int pageNumber = 1 , [FromQuery] int PageSize = 10)
        {
            try
            {
                return Ok(await _productServices.ProductPagination(pageNumber, PageSize));
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
            
        }


        [HttpGet("{id}", Name ="getproducts")]

        public async Task<ActionResult> GetProdectById(int id)
        {
            try
            {
                var products = await _productServices.GetProductById(id);
                return Ok(products);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpGet("paginated-by-category")]

        public async Task<ActionResult> paginatedByCategory(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                return Ok(await _productServices.paginatedByCategory(categoryId,pageNumber, pageSize));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("product-by-category")]

        public async Task<ActionResult> GetProductByCategory(string categoryName)
        {
            try
            {
                return Ok(await _productServices.GetProductByCategory(categoryName));
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto , IFormFile image)
        {
            try
            {
                await _productServices.AddProduct(productDto, image);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productServices.DeleteProduct(id);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto, IFormFile image)
        {
            try
            {
                await _productServices.UpdateProduct(id, productDto, image);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpGet("serach-product")]

        public async Task<ActionResult> SearchProduct(string searchTerm)
        {
            try
            {
                return Ok(await _productServices.SearchProduct(searchTerm));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("get-product-image")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetProductImage(int productId)
        {
            try
            {
                return Ok(await _productServices.GetProductImage(productId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }








    }
}
