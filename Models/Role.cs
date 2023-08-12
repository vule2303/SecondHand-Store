using System;
using System.Collections.Generic;

namespace MVC_Core.Model2;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
