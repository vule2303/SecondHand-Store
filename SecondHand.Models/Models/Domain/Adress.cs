using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SecondHand.Models.Domain { 

public partial class Adress
{
    public int Id { get; set; }

    public int? AdressId { get; set; }
    [Column(TypeName = "nvarchar")]
    [StringLength(450)]
    public string? UserId { get; set; }

    public virtual UserAddress? UserAddress { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
}