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

        public decimal Discount { get; set; }
    
        public decimal Total { get; set; }

        public string? Note { get; set; }

        public DateTime? ShippingDate { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get;set; }
        public string? Ward { get; set; }
        
        public DateTime? OrderDate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Promotion? Promotion { get; set; }
    }
}