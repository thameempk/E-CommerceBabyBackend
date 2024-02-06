namespace BabyBackend.Models.Dto
{
    public class CartViewDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal TotalAmount { get; set; }
        
        public int Quantity {  get; set; }
    }
}
