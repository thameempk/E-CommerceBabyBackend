using BabyBackend.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

        [HttpGet("get-cart-items")]
        [Authorize]
        public async Task<ActionResult> GetCartItems()
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1]; 

                return Ok(await _cartServices.GetCartItems(jwtToken));
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }

        [HttpPost("add-to-cart")]
        [Authorize]
        public async Task<ActionResult> AddToCart( int productId )
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                await _cartServices.AddToCart(jwtToken, productId);
                return Ok("product successfully added to the cart");
            }catch( Exception ex )
            {
                return StatusCode(500,ex.Message);
            }
           
        }

        [HttpPut("add-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityAdd( int productId)
        {
            try
            {
                
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                await _cartServices.QuantityPlus(jwtToken, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpPut("min-quantity")]
        [Authorize]
        public async Task<IActionResult> QuantityMin( int productId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                await _cartServices.QuantityMin(jwtToken, productId);
                return Ok();
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
            
        }

        [HttpDelete("remove-cart-item")]
        [Authorize]
        public async Task<ActionResult> RemoveCartItem( int productId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                await _cartServices.DeleteCart(jwtToken, productId);
                return Ok(true);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
                    
        }



    }
}
