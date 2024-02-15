using System.ComponentModel.DataAnnotations;

namespace BabyBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<WhishList> WhishLists { get; set; }

        public virtual List<CartItem> CartItems { get; set; }

    }
}
