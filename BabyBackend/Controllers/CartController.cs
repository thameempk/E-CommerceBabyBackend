using BabyBackend.Services.CartService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> GetCartItems( string token)
        {
            try
            {
                return Ok(await _cartServices.GetCartItems(token));
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }

        [HttpPost("add-to-cart")]
        [Authorize]
        public async Task<ActionResult> AddToCart([FromBody] string token, int productId )
        {
            try
            {
                await _cartServices.AddToCart( token, productId);
                return Ok();
            }catch( Exception ex )
            {
                return StatusCode(500,ex.Message);
            }
           
        }

        [HttpPut("add-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityAdd([FromBody] string token, int productId)
        {
            try
            {
                await _cartServices.QuantityPlus( token, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPut("min-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityMin([FromBody]string token, int productId)
        {
            try
            {
                await _cartServices.QuantityMin( token, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpDelete("remove-cart-item")]
        [Authorize]
        public async Task<ActionResult> RemoveCartItem([FromBody] string token, int productId)
        {
            try
            {
                await _cartServices.DeleteCart( token, productId);
                return Ok(true);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
                    
        }



    }
}
