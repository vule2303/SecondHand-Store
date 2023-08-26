using System;
using System.Collections.Generic;

namespace SecondHand.Models.Domain { 

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modifield { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<CategoryDiscount> CategoryDiscounts { get; set; } = new List<CategoryDiscount>();

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
}