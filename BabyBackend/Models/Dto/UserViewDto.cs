namespace BabyBackend.Models.Dto
{
    public class UserViewDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }

        public bool IsBlocked { get; set; }
    }
}
