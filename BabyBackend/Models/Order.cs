﻿namespace BabyBackend.Models
{
    public class OrderMain
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerCity { get; set; }

        public string HomeAddress { get; set; }

        public string OrderStatus { get; set; }

        public string OrderString { get; set; }

        public string TransactionId { get; set; }

        public Users users { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
