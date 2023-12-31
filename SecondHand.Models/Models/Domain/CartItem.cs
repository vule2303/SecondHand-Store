﻿using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondHand.Models.Domain { 

public class CartItem
    {
        public CartItem() 
        { 
            count = 1;
   
        }

        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
            [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

   
        public int count { get; set; }
        [NotMapped]
        public decimal price { get; set; }
    }
}