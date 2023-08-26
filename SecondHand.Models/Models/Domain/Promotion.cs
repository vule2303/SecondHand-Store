using System;
using System.Collections.Generic;

namespace SecondHand.Models.Domain { 

public partial class Promotion
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? DiscountType { get; set; }

    public decimal? DiscountValue { get; set; }

    public int MiniumOrderAmount { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modifield { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
}