namespace BabyBackend.Models
{
    public class WhishList
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int ProductId { get; set; }

        public virtual Users users { get; set; }
        public virtual Product products { get; set; }
    }
}
