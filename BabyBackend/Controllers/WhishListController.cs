using BabyBackend.Services.WhishListService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhishListController : ControllerBase
    {
        private readonly IWhishListServices _whishLis;

        public WhishListController(IWhishListServices whishLis)
        {
            _whishLis = whishLis;
        }

        [HttpGet("get-whishlist")]
        [Authorize]
        public async Task<ActionResult> GetWhishLists(int userId)
        {
            try
            {
                return Ok(await _whishLis.GetWhishLists(userId));
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
           
        }

        [HttpPost("add-whishlist")]
        [Authorize]
        public async Task<ActionResult> AddWhishList(int  userId, int productId)
        {
            try
            {
               var isExist = await _whishLis.AddToWhishList(userId, productId);
                if(!isExist)
                {
                    return BadRequest("item already in the whishList");
                }
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("remove-whishlist")]
        [Authorize]
        public async Task<ActionResult> DeleteWhishList(int productId)
        {
            try
            {
                await _whishLis.RemoveWhishList(productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
           
        }

    }
}
