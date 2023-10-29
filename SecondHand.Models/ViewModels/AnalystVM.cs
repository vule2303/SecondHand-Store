using Microsoft.Build.ObjectModelRemoting;
using SecondHand.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.ViewModels
{
    public class AnalystVM
    {

        public int OrderQuantity { get; set; }
        public int UserQuantity { get; set; }
        public decimal TotalSales { get; set; }
        public List<int> TotalOrderPermonth { get;set; }
        public List<decimal> TotalSalesPerMonth { get; set; } 
    }
    
}
