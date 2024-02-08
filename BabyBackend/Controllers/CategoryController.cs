using BabyBackend.Models.Dto;
using BabyBackend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await _categoryServices.GetCategories());
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            try
            {
                return Ok(await _categoryServices.GetCategoryById(id));
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryServices.AddCategory(categoryDto);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
                       
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCategory(int id , [FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryServices.UpdateCategory(id, categoryDto);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
                    
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryServices.DeleteCategory(id);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }
    }
}
