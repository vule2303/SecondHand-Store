using Microsoft.AspNetCore.Identity;

namespace MVC_Core.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get;set; }
    }
}
