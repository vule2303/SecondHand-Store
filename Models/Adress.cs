using System;
using System.Collections.Generic;

namespace MVC_Core.Models;

public partial class Adress
{
    public int Id { get; set; }

    public int? AdressId { get; set; }

    public int? UserId { get; set; }

    public virtual UserAddress? AdressNavigation { get; set; }

    public virtual User? User { get; set; }
}
