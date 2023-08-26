using System;
using System.Collections.Generic;

namespace SecondHand.Models.Domain { 
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool? Status { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public bool? IsNew { get; set; }

    public string? Color { get; set; }

    public string? Conditon { get; set; }

    public string? Defects { get; set; }

    public DateTime? Created { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public string? Img4 { get; set; }

    public string? Img5 { get; set; }

    public string Size { get; set; } = null!;

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
}
