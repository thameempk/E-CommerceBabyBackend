using BabyBackend.Models.Dto;
using BabyBackend.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }


        [HttpGet]

        public ActionResult GetProducts()
        {
           return Ok(_productServices.GetProducts());

        }

        [HttpGet("{id}", Name ="getproducts")]

        public ActionResult GetProdectById(int id)
        {
            return Ok(_productServices.GetProductById(id));
        }


        [HttpGet("product-by-category")]

        public ActionResult GetProductByCategory(int categoryId)
        {
            return Ok(_productServices.GetProductByCategory(categoryId));
        }

        [HttpPost]

        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            _productServices.AddProduct(productDto);
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteProduct(int id)
        {
            _productServices.DeleteProduct(id);
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            _productServices.UpdateProduct(id, productDto);
            return Ok();
        }


    }
}
