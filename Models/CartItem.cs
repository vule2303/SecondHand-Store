using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Models;

public partial class CartItem
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Created { get; set; }
}
