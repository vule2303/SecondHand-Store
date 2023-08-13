﻿using System;
using System.Collections.Generic;

namespace MVC_Core.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
