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

        [HttpPost("create-order")]

        public ActionResult CreateOrder([FromBody] OrderDetailsDto orderDetails)
        {
            string OrderId = _orderServices.OrderCreate(orderDetails);
            return Ok(OrderId);
        }

        [HttpPost("place-Order")]

        public ActionResult PlaceOrder(int userId, List<CartViewDto> cartViews)
        {
            _orderServices.CreateOrder(userId, cartViews);
            return Ok();
        }
    }
}
