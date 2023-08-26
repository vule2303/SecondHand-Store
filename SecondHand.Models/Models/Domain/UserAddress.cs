using System;
using System.Collections.Generic;

namespace SecondHand.Models.Domain {

public partial class UserAddress
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool? Active { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Modifiled { get; set; }

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();
}
}