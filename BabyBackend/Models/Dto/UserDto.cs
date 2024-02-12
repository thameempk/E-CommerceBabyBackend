using System.ComponentModel.DataAnnotations;

namespace BabyBackend.Models.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
    }
}
