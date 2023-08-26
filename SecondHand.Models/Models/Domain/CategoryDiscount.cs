using System;
using System.Collections.Generic;

namespace SecondHand.Models.Domain { 

public partial class CategoryDiscount
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Code { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public bool IsActive { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Category Category { get; set; } = null!;
}
}