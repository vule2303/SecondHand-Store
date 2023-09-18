using Microsoft.AspNetCore.Http;
using SecondHand.Models.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SecondHand.Models.Domain { 
public partial class Product
{
    public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "Mô tả")]
    public string? Description { get; set; }
        [Required]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }
        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }
        [Display(Name = "Danh mục")]
        public int? CategoryId { get; set; }
        [Display(Name = "Thương hiệu")]
        public int? BrandId { get; set; }
        [Display(Name = "Màu sắc")]
        public string? Color { get; set; }
        [Display(Name = "Tình trạng sản phẩm")]
        public string? Conditon { get; set; }
        [Display(Name = "Khiếm khuyết")]
        public string? Defects { get; set; }
        [Display(Name = "Ngày đăng")]
        public DateTime? Created { get; set; }


        [Display(Name = "Size")]
        public string Size { get; set; } = null!;

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [Display(Name = "Tải ảnh lên")]
    [NotMapped]
    public IFormFileCollection? MultipleImages { get; set; }
    [NotMapped]
    public List<ProductImages>? ProductImages { get; set; }
    public virtual ICollection<ProductImages>? productGallery { get; set; } = new List<ProductImages>();

    }
}
