using Microsoft.AspNetCore.Identity;
using SecondHand.Models.Domain;

namespace SecondHand.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Created { get; set; }
        public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
