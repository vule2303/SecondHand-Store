using System;
using System.Collections.Generic;

namespace MVC_Core.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public bool? Status { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modifiled { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
