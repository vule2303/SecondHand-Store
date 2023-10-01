using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Crmf;
using Microsoft.Build.ObjectModelRemoting;

namespace SecondHand.Models.Domain { 

    public partial class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(450)]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int? PromotionId { get; set; }

        public string? PaymentStatus { get; set; }

        public string? PaymentMethod { get; set; }

        public string? OrderStatus { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? Discount { get; set; }
        [Required]
        public decimal? Total { get; set; }

        public string? Note { get; set; }

        public DateTime? ShppingDate { get; set; }

        public bool? IsCancelled { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Cirty { get; set; }
        public string? State { get;set; }
        public string? Name { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Promotion? Promotion { get; set; } 
    }
}