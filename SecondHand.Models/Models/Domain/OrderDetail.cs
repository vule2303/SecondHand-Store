using Microsoft.Build.ObjectModelRemoting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondHand.Models.Domain { 

public partial class OrderDetail
{
        [Key]
    public int Id { get; set; }
        [Required]
    public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public int count { get; set; }
        public double Price {get; set; }
    

}
}