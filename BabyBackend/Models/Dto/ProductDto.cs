﻿namespace BabyBackend.Models.Dto
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price {  get; set; }

        public int categoryId { get; set; }
    }
}
