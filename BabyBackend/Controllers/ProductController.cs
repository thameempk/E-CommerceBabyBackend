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

        public ActionResult GetProducts()
        {
            var products = _productServices.GetProducts();
            if(products != null && products.Count > 0)
            {
                foreach (var p in products)
                {
                    p.ProductImage = GetImageById(p.Id);
                }
            }
           return Ok(products);

        }

        [HttpGet("{id}", Name ="getproducts")]

        public ActionResult GetProdectById(int id)
        {
            var products = _productServices.GetProductById(id);
            if (products != null)
            {
                products.ProductImage = GetImageById(products.Id);
            }
            return Ok(products);
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

        [HttpPost("UploadImage")]

        public async Task<ActionResult> UploadImage()
        {
            var files = Request.Form.Files;
            foreach(IFormFile file in files)
            {
                string FileName = file.FileName;
                string FilePath = GetFilePath(FileName);

                if(!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }

                string ImagePath = FilePath + "\\image.png";
                if(System.IO.File.Exists(ImagePath))
                {
                    System.IO.File.Delete(ImagePath);
                }

                using(FileStream stream = System.IO.File.Create(ImagePath))
                {
                    await stream.CopyToAsync(stream);
                }

            }
            return Ok();
        }

        [HttpGet("Remove-Image")]

        public ActionResult RemoveImage (int productId)
        {
            string filePath = GetFilePath(productId.ToString());
            string ImagePath = filePath + "\\image.png";

            if (System.IO.File.Exists(ImagePath))
            {
                System.IO.File.Delete(filePath);
            }
            return Ok();
        }

        [NonAction]
        private string GetFilePath(string productId)
        {
            return _webHostEnvironment.WebRootPath + "\\Uploads\\Product\\" + productId;
        }

        [NonAction]

        private string GetImageById(int productId)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "http://localhost:5237/";
            string filePath = GetFilePath(productId.ToString());
            string ImagePath = filePath + "\\image.png";

            if(!System.IO.File.Exists(ImagePath))
            {
                ImageUrl = HostUrl + "Uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "Uploads/Product/" + productId + "/image.png";
            }
            return ImageUrl;
        }



    }
}
