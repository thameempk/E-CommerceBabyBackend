using BabyBackend.DbContexts;
using BabyBackend.Models;
using BabyBackend.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<string> OrderCreate(long price)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            Random random = new Random();
            string TrasactionId = random.Next(0, 1000).ToString();
            input.Add("amount", Convert.ToDecimal(price) * 100);
            input.Add("currency", "INR");
            input.Add("receipt", TrasactionId);

            string key = _configuration["Razorpay:KeyId"];
            string secret = _configuration["Razorpay:KeySecret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order =  client.Order.Create(input);
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

        public async Task<bool> CreateOrder(int userId, OrderRequestDto orderRequests)
        {
            try
            {
                if (orderRequests.TransactionId == null && orderRequests.OrderString == null)
                {
                    return false;
                }
                var order = new OrderMain
                {
                    userId = userId,
                    OrderDate = DateTime.Now,
                    CustomerCity = orderRequests.CustomerCity,
                    CustomerEmail = orderRequests.CustomerEmail,
                    CustomerName = orderRequests.CustomerName,
                    CustomerPhone = orderRequests.CustomerPhone,
                    HomeAddress = orderRequests.HomeAddress,
                    OrderStatus = orderRequests.OrderStatus,
                    OrderString = orderRequests.OrderString,
                    TransactionId = orderRequests.TransactionId,
                    OrderItems = orderRequests.CartViews.Select(cv => new OrderItem
                    {
                        ProductId = cv.ProductId,
                        Quantity = cv.Quantity,
                        TotalPrice = cv.Quantity * cv.Price
                    }).ToList()
                };

                _dbContext.orders.Add(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<OrderViewDto>> GetOrderDtails(int userId)
        {
            var order = await _dbContext.orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.userId == userId);

            if (order != null)
            {
                var orderDetails = order.OrderItems.Select(o => new OrderViewDto
                {
                    Id = o.Id,
                    OrderDate = o.Order.OrderDate,
                    ProductName = o.Product.ProductName,
                    ProductImage = o.Product.ProductImage,
                    Quantity = o.Quantity,
                    TotalPrice = o.TotalPrice,
                    OrderId = order.OrderString,
                    OrderStatus = order.OrderStatus
                }).ToList();

                return orderDetails;
            }

            return new List<OrderViewDto>();
        }

        public async Task<List<OrderAdminViewDto>> GetOrderDetailAdmin()
        {
            var orders = await _dbContext.orders.Include(o => o.OrderItems).ToListAsync();

            if (orders != null)
            {
                var orderdet = orders.Select(o => new OrderAdminViewDto
                {
                    Id = o.Id,
                    CustomerEmail = o.CustomerEmail,
                    CustomerName = o.CustomerName,
                    OrderId = o.OrderString,
                    TransactionId = o.TransactionId,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus
                }).ToList();

                return orderdet;
            }

            return new List<OrderAdminViewDto>();
        }

        public async Task<OrderAdminDetailViewDto> GetOrderDetailsById(int orderId)
        {
            var orders = await _dbContext.orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (orders != null)
            {
                var orderdet = new OrderAdminDetailViewDto
                {
                    Id = orderId,
                    CustomerEmail = orders.CustomerEmail,
                    CustomerName = orders.CustomerName,
                    CustomerCity = orders.CustomerCity,
                    OrderStatus = orders.OrderStatus,
                    CustomerPhone = orders.CustomerPhone,
                    OrderString = orders.OrderString,
                    HomeAddress = orders.HomeAddress,
                    TransactionId = orders.TransactionId,
                    OrderDate = orders.OrderDate,
                    orderProducts = orders.OrderItems.Select(oi => new CartViewDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.ProductName,
                        Price = oi.Product.Price,
                        Quantity = oi.Quantity,
                        TotalAmount = oi.TotalPrice,
                        ProductImage = oi.Product.ProductImage
                        
                    }).ToList()
                };
                return orderdet;
            }

            return new OrderAdminDetailViewDto();
        }

        public async Task<decimal> GetTotalRevenue()
        {
            var order = await _dbContext.orders.Include(o => o.OrderItems).ToListAsync();

            if (order != null)
            {
                var orderdet = order.SelectMany(o => o.OrderItems);
                var totalIncome = orderdet.Sum(od => od.TotalPrice);
                return totalIncome;
            }

            return 0;
        }

        public async Task<bool> UpdateOrder(int orderId, OrderAdminViewDto orderAdminView)
        {
            var order = await _dbContext.orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = orderAdminView.OrderStatus;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
