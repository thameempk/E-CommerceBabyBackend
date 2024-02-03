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

        public ActionResult GetWhishLists(int userId)
        {
            return Ok(_whishLis.GetWhishLists(userId));
        }

        [HttpPost("add-whishlist")]

        public ActionResult AddWhishList(int  userId, int productId)
        {
            _whishLis.AddToWhishList(userId, productId);
            return Ok();
        }

        [HttpDelete("remove-whishlist")]

        public ActionResult DeleteWhishList(int wId)
        {
            _whishLis.RemoveWhishList(wId);
            return Ok();
        }

    }
}
