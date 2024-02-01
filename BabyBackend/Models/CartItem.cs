﻿namespace BabyBackend.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity {  get; set; }

        public virtual Cart cart { get; set; }
        public virtual Product product { get; set; }

    }
}
