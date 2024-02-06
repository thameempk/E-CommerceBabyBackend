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
        public string  OrderCreate(long price)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            Random random = new Random();
            string TrasactionId = random.Next(0, 1000).ToString();
            input.Add("amount", Convert.ToDecimal(price)*100);
            input.Add("currency", "INR");
            input.Add("receipt", TrasactionId);

            string key = _configuration["Razorpay:KeyId"];
            string secret = _configuration["Razorpay:KeySecret"];
                   
            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            var OrderId = order["id"].ToString();
            
            return OrderId;
        }

        public List<OrderDetailsDto> Payment(RazorpayDto razorpay)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("raz_pay_id", razorpay.raz_pay_id);
            attributes.Add("raz_ord_id", razorpay.raz_ord_id);
            attributes.Add("raz_pay_sig", razorpay.raz_pay_sig);

            Utils.verifyPaymentSignature(attributes);
            List<OrderDetailsDto> orderList = new List<OrderDetailsDto>
            {
               new OrderDetailsDto 
               {
                TransactionId = razorpay.raz_pay_id,
                OrderId = razorpay.raz_ord_id
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
                    Quantity = cv.Quantity,
                    TotalPrice = cv.Price * cv.Quantity
                }).ToList()

            };

            _dbContext.orders.Add(order);
            _dbContext.SaveChanges();

            
        }

        public List<OrderViewDto> GetOrderDtails(int userId)
        {
            var order = _dbContext.orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).FirstOrDefault(o => o.userId == userId);

            if(order != null )
            {
                var orderDetails = order.OrderItems.Select(o => new OrderViewDto
                {
                  
                        Id = o.Id,
                        OrderDate = o.Order.OrderDate,
                        ProductName = o.Product.ProductName,
                        Quantity = o.Quantity,
                        TotalPrice = o.TotalPrice,
                    
                }).ToList();

                return orderDetails;
            }

            return new List<OrderViewDto>();
        }


        public decimal GetTotalRevenue()
        {
            var order = _dbContext.orders.Include(o => o.OrderItems);

            if(order != null)
            {
                var orderdet = order.SelectMany(o => o.OrderItems);
                var totalIncome = orderdet.Sum(od => od.TotalPrice);

                return totalIncome;

            }
            return 0;
        }



    }
}
