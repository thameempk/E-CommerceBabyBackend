using System.ComponentModel.DataAnnotations;

namespace BabyBackend.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal TotalPrice {  get; set; }
        [Required]
        public int Quantity { get; set; }

        

        public virtual OrderMain Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
