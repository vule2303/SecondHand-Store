using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Utility.Helper
{
    public class PagingModel
    {
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public Func<int?, string> generateURL { get; set; }
    }
}
