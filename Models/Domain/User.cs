using Microsoft.AspNetCore.Identity;
using Microsoft.Build.ObjectModelRemoting;

namespace MVC_Core.Models.Domain
{
    public class User:IdentityUser
    {
        public string Name { get;set; }
        public string Email { get;set; }
        public string UserName { get; set; }
        public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
