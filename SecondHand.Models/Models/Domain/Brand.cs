using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondHand.Models.Domain { 

public partial class Brand
{
    [Key]

    public int Id { get; set; }
    [DisplayName("Tên thương hiệu")]
    public string? Name { get; set; }

    public string? Logo { get; set; }
    [DisplayName("Mô tả thương hiệu")]
    public string? Description { get; set; }
    [NotMapped]
    [DisplayName("Tải ảnh lên")]
    public IFormFile? ImageFile { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
}