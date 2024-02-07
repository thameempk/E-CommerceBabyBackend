namespace BabyBackend.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public decimal TotalPrice {  get; set; }
        public int Quantity { get; set; }

        

        public virtual OrderMain Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
