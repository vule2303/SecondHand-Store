using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Crmf;
using Microsoft.Build.ObjectModelRemoting;
using SecondHand.Models.Models.Domain;

namespace SecondHand.Models.Domain { 

    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderCode { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int? PromotionId { get; set; }

        public string? PaymentStatus { get; set; }

        public string? PaymentMethod { get; set; }

        public string? OrderStatus { get; set; }

        public decimal Subtotal { get; set; }
        public decimal FeeShipping { get; set; }
        public decimal Discount { get; set; }
    
        public decimal Total { get; set; }

        public string? Note { get; set; }

        public DateTime? ShippingDate { get; set; }
        [Required(ErrorMessage = "Phải nhập {0} đăng nhập")]
        [Display(Name = "Tên người nhận")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Nhập {0}")]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }
       
        [Required(ErrorMessage = "Nhập {0}")]
        [Display(Name = "Địa chỉ giao hàng")]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get;set; }
        [Required]
        public string? Ward { get; set; }
        
        public DateTime? OrderDate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Promotion? Promotion { get; set; }
    }
}