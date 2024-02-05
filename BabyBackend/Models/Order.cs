namespace BabyBackend.Models
{
    public class OrderMain
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public DateTime OrderDate { get; set; }

        public Users users { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
