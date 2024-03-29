﻿using BabyBackend.DbContexts;
using BabyBackend.JwtVerification;
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
        private readonly string HostUrl;
        private readonly IJwtServices _jwtServices;

        public OrderServices(IConfiguration configuration, BabyDbContext dbContext, IJwtServices jwtServices)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            HostUrl = _configuration["HostUrl:url"];
            _jwtServices = jwtServices;
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

        public bool Payment(RazorpayDto razorpay)
        {
            if (razorpay == null ||
        razorpay.razorpay_payment_id == null ||
        razorpay.razorpay_order_id == null ||
        razorpay.razorpay_signature == null)
            {
                return false;
            }
            RazorpayClient client = new RazorpayClient(_configuration["Razorpay:KeyId"], _configuration["Razorpay:KeySecret"]);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", razorpay.razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay.razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay.razorpay_signature);

            Utils.verifyPaymentSignature(attributes);


            return true;
        }

        public async Task<bool> CreateOrder(string token, OrderRequestDto orderRequests)
        {
            try
            {
                int userId = _jwtServices.GetUserIdFromToken(token);

                if (userId == null)
                {
                    throw new Exception("user id not valid");
                }

                if (orderRequests.TransactionId == null && orderRequests.OrderString == null)
                {
                    return false;
                }
                var cart = await _dbContext.cart.Include(c => c.cartItems).ThenInclude(ci=>ci.product).FirstOrDefaultAsync(c=>c.UserId == userId);
                var order = new OrderMain
                {
                    userId = userId,
                    OrderDate = DateTime.Now,
                    CustomerCity = orderRequests.CustomerCity,
                    CustomerEmail = orderRequests.CustomerEmail,
                    CustomerName = orderRequests.CustomerName,
                    CustomerPhone = orderRequests.CustomerPhone,
                    HomeAddress = orderRequests.HomeAddress,
                    OrderString = orderRequests.OrderString,
                    TransactionId = orderRequests.TransactionId,
                    OrderItems = cart.cartItems.Select(cv => new OrderItem
                    {
                        ProductId = cv.ProductId,
                        Quantity = cv.Quantity,
                        TotalPrice = cv.Quantity * cv.product.Price
                    }).ToList()
                };

                _dbContext.orders.Add(order);
                _dbContext.cart.Remove(cart);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<OrderViewDto>> GetOrderDtails(string token)
        {
            int userId = _jwtServices.GetUserIdFromToken(token);

            if (userId == null)
            {
                throw new Exception("user id not valid");
            }

            var orders = await _dbContext.orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .Where(o => o.userId == userId)
                .ToListAsync();

            var orderDetails = new List<OrderViewDto>();

            foreach (var order in orders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var orderDetail = new OrderViewDto
                    {
                        Id = orderItem.Id,
                        OrderDate = order.OrderDate,
                        ProductName = orderItem.Product.ProductName,
                        ProductImage = HostUrl + orderItem.Product.ProductImage,
                        Quantity = orderItem.Quantity,
                        TotalPrice = orderItem.TotalPrice,
                        OrderId = order.OrderString,
                        OrderStatus = order.OrderStatus
                    };

                    orderDetails.Add(orderDetail);
                }
            }

            return orderDetails;
        }

        public async Task<List<OrderViewDto>> adminUserOrder(int userId)
        {
            if (userId == null)
            {
                throw new Exception("user id not valid");
            }
            var orders = await _dbContext.orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .Where(o => o.userId == userId)
                .ToListAsync();

            var orderDetails = new List<OrderViewDto>();

            foreach (var order in orders)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var orderDetail = new OrderViewDto
                    {
                        Id = orderItem.Id,
                        OrderDate = order.OrderDate,
                        ProductName = orderItem.Product.ProductName,
                        ProductImage = HostUrl + orderItem.Product.ProductImage,
                        Quantity = orderItem.Quantity,
                        TotalPrice = orderItem.TotalPrice,
                        OrderId = order.OrderString,
                        OrderStatus = order.OrderStatus
                    };

                    orderDetails.Add(orderDetail);
                }
            }

            return orderDetails;
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
                        ProductName =  oi.Product.ProductName,
                        Price = oi.Product.Price,
                        Quantity = oi.Quantity,
                        TotalAmount = oi.TotalPrice,
                        ProductImage = HostUrl + oi.Product.ProductImage
                        
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

        public async Task<bool> UpdateOrder(int orderId, UpdateOrderDto orderAdminView)
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

        public async Task<int> GetTotalOrders()
        {
            var order = await _dbContext.orders.Include(o => o.OrderItems).ToListAsync();

            if (order != null)
            {
                var orderdet = order.SelectMany(o => o.OrderItems);
                var totalIncome = orderdet.Sum(od => od.Quantity);
                return totalIncome;
            }

            return 0;
        }

        public async Task<int> TodayOrders()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var order = await _dbContext.orders.Include(o => o.OrderItems).Where(o => o.OrderDate >= todayStart && o.OrderDate <= todayEnd).ToListAsync();

            if (order != null)
            {
                var orderdet = order.SelectMany(o => o.OrderItems);
                int todayOrders = orderdet.Sum(od => od.Quantity);
                return todayOrders;
            }
            return 0;

        }

        public async Task<decimal> TodayRevenue()
        {
            DateTime todayStart = DateTime.Today;
            DateTime todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var order = await _dbContext.orders.Include(o => o.OrderItems).Where(o=>o.OrderDate >= todayStart && o.OrderDate <= todayEnd).ToListAsync();

            if (order != null)
            {
                var orderdet = order.SelectMany(o => o.OrderItems);
                decimal totalIncome = orderdet.Sum(od => od.TotalPrice);
                return totalIncome;
            }
            return 0;
        }

    }
}
