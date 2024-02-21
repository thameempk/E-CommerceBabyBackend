using BabyBackend.Services.WhishListService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public async Task<ActionResult> GetWhishLists()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                return Ok(await _whishLis.GetWhishLists(jwtToken));
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
           
        }

        [HttpPost("add-whishlist")]
        [Authorize]
        public async Task<ActionResult> AddWhishList( int productId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                var isExist = await _whishLis.AddToWhishList(jwtToken, productId);
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
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                await _whishLis.RemoveWhishList(jwtToken,productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
           
        }

        

    }
}
