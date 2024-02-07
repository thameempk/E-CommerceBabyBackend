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
            return Ok(await _whishLis.GetWhishLists(userId));
        }

        [HttpPost("add-whishlist")]

        public async Task<ActionResult> AddWhishList(int  userId, int productId)
        {
            await _whishLis.AddToWhishList(userId, productId);
            return Ok();
        }

        [HttpDelete("remove-whishlist")]

        public async Task<ActionResult> DeleteWhishList(int wId)
        {
            await _whishLis.RemoveWhishList(wId);
            return Ok();
        }

    }
}
