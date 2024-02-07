using BabyBackend.Models.Dto;
using BabyBackend.Services.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }


        [HttpPost("order-create")]

        public async Task<ActionResult> createOrder(long price)
        {
            var orderId = await _orderServices.OrderCreate(price);
            return Ok(orderId);

        }


        [HttpPost("payment")]

        public  ActionResult Payment(RazorpayDto razorpay)
        {
            var con = _orderServices.Payment(razorpay);
            return Ok(con);
        }


        [HttpPost("place-Order")]

        public async Task<ActionResult> PlaceOrder(int userId, OrderRequestDto orderRequests)
        {
            await _orderServices.CreateOrder(userId, orderRequests);
            return Ok();
        }



        [HttpGet("get_order_details")]

        public async Task<ActionResult> GetOrderDetails (int userId)
        {
            
            return Ok(await _orderServices.GetOrderDtails(userId));
        }

        [HttpGet("total_revenue")]

        public async Task<ActionResult> GetTotalRevenue()
        {
            return Ok(await _orderServices.GetTotalRevenue());
        }

        [HttpGet("get-order-details-admin")]

        public async Task<ActionResult> GetOrderDetailsAdmin()
        {
            return Ok(await _orderServices.GetOrderDetailAdmin());

        }

        [HttpGet("get-detailed-order")]

        public async Task<ActionResult> GetDetailedOrder(int orderId)
        {
            return Ok(await _orderServices.GetOrderDetailsById(orderId));
        }
    }
}
