namespace BabyBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public virtual Users users { get; set; }
        public virtual List<CartItem> cartItems { get; set; }
    }
}
