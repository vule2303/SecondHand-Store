using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Models;

public partial class Adress
{
    public int Id { get; set; }

    public int? AdressId { get; set; }
    [Column(TypeName = "nvarchar")]
    [StringLength(450)]
    public string? UserId { get; set; }

    public virtual UserAddress? UserAddress { get; set; }

    public virtual User? User { get; set; }
}
