using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SecondHand.Models.Domain { 

public partial class Order
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar")]
    [StringLength(450)]
    public string? UserId { get; set; }

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

    public virtual ApplicationUser? User { get; set; }
}
}