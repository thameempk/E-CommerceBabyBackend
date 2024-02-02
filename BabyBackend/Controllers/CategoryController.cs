using BabyBackend.Models.Dto;
using BabyBackend.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            return Ok(_categoryServices.GetCategories());
        }

        [HttpGet("{id}")]

        public ActionResult GetCategoryById(int id)
        {
            return Ok(_categoryServices.GetCategoryById(id));
        }

        [HttpPost]

        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            _categoryServices.AddCategory(categoryDto);
            return Ok();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateCategory(int id , [FromBody] CategoryDto categoryDto)
        {
            _categoryServices.UpdateCategory(id, categoryDto);
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteCategory(int id)
        {
            _categoryServices.DeleteCategory(id);
            return Ok();
        }
    }
}
