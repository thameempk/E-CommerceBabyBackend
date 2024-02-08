using System.ComponentModel.DataAnnotations;

namespace BabyBackend.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string Role { get; set; }

        public bool isBlocked {  get; set; }
        public virtual Cart cart { get; set; }

        public virtual List<OrderMain> Orders { get; set; }
        public virtual List<WhishList> whishLists { get; set; }
        
    }
}
