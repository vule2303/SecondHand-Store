using System;
using System.Collections.Generic;

namespace MVC_Core.Model2;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PromotionId { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentMethod { get; set; }

    public string? OrderStatus { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Disscount { get; set; }

    public decimal? Total { get; set; }

    public string? Note { get; set; }

    public DateTime? Created { get; set; }

    public bool? IsCancelled { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Promotion? Promotion { get; set; }

    public virtual User? User { get; set; }
}
