using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondHand.Models.Domain { 
[PrimaryKey(nameof(UserId), nameof(ProductId))]
public partial class Favorite
{
    [Column(TypeName = "nvarchar")]
    [StringLength(450)]
    public string UserId { get; set; } = default;

    public int ProductId { get; set; }

    public DateTime? Created { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
}