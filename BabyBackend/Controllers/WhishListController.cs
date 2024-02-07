using BabyBackend.Services.WhishListService;
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

        public async Task<ActionResult> AddWhishList(int  userId, int productId)
        {
            try
            {
                await _whishLis.AddToWhishList(userId, productId);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("remove-whishlist")]

        public async Task<ActionResult> DeleteWhishList(int wId)
        {
            try
            {
                await _whishLis.RemoveWhishList(wId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
           
        }

    }
}
