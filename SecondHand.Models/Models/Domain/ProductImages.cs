using MimeKit.Cryptography;
using SecondHand.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.Models.Domain
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  
        public string Name { get; set; }
        public string URL { get; set; } 

        public Product Products { get; set; }
    }
}
