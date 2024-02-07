namespace BabyBackend.Models.Dto
{
    public class OrderViewDto
    {
        public int Id { get; set; } 

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
        
        public DateTime OrderDate {  get; set; }

        public string OrderId {  get; set; }

        public string OrderStatus {  get; set; }

    }
}
