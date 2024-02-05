using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Net.WebSockets;

namespace BabyBackend.Services.OrderService
{
    public class OrderServices : IOrderServices
    {
        private readonly IConfiguration _configuration;
        private readonly BabyDbContext _dbContext;

        public OrderServices(IConfiguration configuration, BabyDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public string OrderCreate(OrderDetailsDto orderDetails)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            Random random = new Random();
            string TrasactionId = random.Next(0, 1000).ToString();
            input.Add("amount", Convert.ToDecimal( orderDetails.TotalAmount)*100); 
            input.Add("currency", "INR");
            input.Add("receipt", TrasactionId);

            string key = _configuration["Razorpay:KeyId"];
            string secret = _configuration["Razorpay:KeySecret"];

           
           

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            string OrderId = order["id"].ToString();
            return OrderId;
        }

        public List<OrderDetailsDto> Payment(string raz_pay_id, string raz_ord_id, string raz_pay_sig)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("raz_pay_id", raz_pay_id);
            attributes.Add("raz_ord_id", raz_ord_id);
            attributes.Add("raz_pay_sig", raz_pay_sig);

            Utils.verifyPaymentSignature(attributes);
            List<OrderDetailsDto> orderList = new List<OrderDetailsDto>
            {
               new OrderDetailsDto 
               {
                TransactionId = raz_pay_id,
                OrderId = raz_ord_id
               }
        };
            

            return orderList;
        }


        public void CreateOrder(int userId, List<CartViewDto> cartViews)
        {

            var order = new OrderMain
            {
                userId = userId,
                OrderDate = DateTime.Now,

                OrderItems = cartViews.Select(cv => new OrderItem
                {
                    ProductId = cv.ProductId,
                    Quantity = cv.Quantity
                }).ToList()

            };

            _dbContext.orders.Add(order);
            _dbContext.SaveChanges();
            
        }
    }
}
