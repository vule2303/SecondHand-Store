using System;
using System.Collections.Generic;

namespace MVC_Core.Model2;

public partial class CartItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Created { get; set; }
}
