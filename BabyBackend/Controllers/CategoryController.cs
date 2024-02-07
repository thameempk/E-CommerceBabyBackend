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
        public async Task<ActionResult> GetCategories()
        {
            return Ok(await _categoryServices.GetCategories());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetCategoryById(int id)
        {
            return Ok(await _categoryServices.GetCategoryById(id));
        }

        [HttpPost]

        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryServices.AddCategory(categoryDto);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCategory(int id , [FromBody] CategoryDto categoryDto)
        {
            await _categoryServices.UpdateCategory(id, categoryDto);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryServices.DeleteCategory(id);
            return Ok();
        }
    }
}
