using SecondHand.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.Models.Domain
{
    public class PaymentDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ResponseCode { get; set; }

    }
}
