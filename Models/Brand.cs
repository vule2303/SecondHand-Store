using System;
using System.Collections.Generic;

namespace MVC_Core.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Logo { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
