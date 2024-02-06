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

        [HttpGet("{userId}")]

        public ActionResult GetCartItems(int userId)
        {
            return Ok(_cartServices.GetCartItems(userId));
        }

        [HttpPost("add-to-cart")]
        public ActionResult AddToCart(int userId, int productId )
        {
            _cartServices.AddToCart(userId, productId);
            return Ok();
        }

        [HttpPut("add-quantity")]
        public IActionResult QuantityAdd(int userId, int productId)
        {
            _cartServices.QuantityPlus(userId, productId);
            return Ok();
        }

        [HttpPut("min-quantity")]

        public IActionResult QuantityMin(int userId, int productId)
        {
            _cartServices.QuantityMin(userId, productId);
            return Ok();
        }

        [HttpDelete("remove-cart-item")]

        public ActionResult RemoveCartItem(int userId, int productId)
        {
            _cartServices.DeleteCart( userId,  productId);
            return Ok(true);
        }



    }
}
