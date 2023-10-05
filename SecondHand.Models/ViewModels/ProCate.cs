using SecondHand.Models.Domain;
using SecondHand.Models.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.ViewModels
{
    public class ProCate
    {
        public Product DanhMuc{ get; set; }
        public Brand ThuongHieu { get; set; }
      
        public List<ProductImages> AnhSP { get; set; }
    }
}
