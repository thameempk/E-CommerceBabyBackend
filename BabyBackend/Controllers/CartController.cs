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

        public async Task<ActionResult> GetCartItems(int userId)
        {
            try
            {
                return Ok(await _cartServices.GetCartItems(userId));
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }

        [HttpPost("add-to-cart")]
        public async Task<ActionResult> AddToCart(int userId, int productId )
        {
            try
            {
                await _cartServices.AddToCart(userId, productId);
                return Ok();
            }catch( Exception ex )
            {
                return StatusCode(500,ex.Message);
            }
           
        }

        [HttpPut("add-quantity")]
        public async Task<IActionResult> QuantityAdd(int userId, int productId)
        {
            try
            {
                await _cartServices.QuantityPlus(userId, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPut("min-quantity")]

        public async Task<IActionResult> QuantityMin(int userId, int productId)
        {
            try
            {
                await _cartServices.QuantityMin(userId, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpDelete("remove-cart-item")]

        public async Task<ActionResult> RemoveCartItem(int userId, int productId)
        {
            try
            {
                await _cartServices.DeleteCart(userId, productId);
                return Ok(true);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
                    
        }



    }
}
