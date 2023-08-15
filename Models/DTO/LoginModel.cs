using Microsoft.Build.Framework;

namespace MVC_Core.Models.DTO
{
    public class LoginModel

    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
