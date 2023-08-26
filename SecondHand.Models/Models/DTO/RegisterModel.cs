using System.ComponentModel.DataAnnotations;
using Required = System.ComponentModel.DataAnnotations;
namespace SecondHand.Models.DTO
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordComfirm { get; set; }
      
        public string? Role { get; set; }
    }
}
