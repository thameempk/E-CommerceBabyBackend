using System.ComponentModel.DataAnnotations;

namespace BabyBackend.Models
{
    public class WhishList
    {
        public int Id { get; set; }
        [Required]
        public int UserId {  get; set; }
        [Required]
        public int ProductId { get; set; }

        public virtual Users users { get; set; }
        public virtual Product products { get; set; }
    }
}
