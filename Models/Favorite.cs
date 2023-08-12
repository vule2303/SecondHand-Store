using System;
using System.Collections.Generic;

namespace MVC_Core.Model2;

public partial class Favorite
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime? Created { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
