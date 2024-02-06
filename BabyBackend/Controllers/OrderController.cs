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

        public ActionResult createOrder(long price)
        {
            var orderId =_orderServices.OrderCreate(price);
            return Ok(orderId);

        }


        [HttpPost("payment")]

        public ActionResult Payment(RazorpayDto razorpay)
        {
            var con = _orderServices.Payment(razorpay);
            return Ok(con);
        }


        [HttpPost("place-Order")]

        public ActionResult PlaceOrder(int userId, List<CartViewDto> cartViews)
        {
            _orderServices.CreateOrder(userId, cartViews);
            return Ok();
        }



        [HttpGet("get_order_details")]

        public ActionResult GetOrderDetails (int userId)
        {
            
            return Ok(_orderServices.GetOrderDtails(userId));
        }

        [HttpGet("total_revenue")]

        public ActionResult GetTotalRevenue()
        {
            return Ok(_orderServices.GetTotalRevenue());
        }
    }
}
