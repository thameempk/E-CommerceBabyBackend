namespace BabyBackend.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public virtual Cart cart { get; set; }
        
    }
}
