using BabyBackend.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpPost("add-to-cart")]
        public ActionResult AddToCart(int userId, int productId )
        {
            _cartServices.AddToCart(userId, productId);
            return Ok();
        }

        [HttpPut]
        public IActionResult QuantityAdd(int userId, int productId)
        {
            _cartServices.QuantityPlus(userId, productId);
            return Ok();
        }
    }
}
