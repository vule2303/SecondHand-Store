using Org.BouncyCastle.Bcpg.OpenPgp;
using SecondHand.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.ViewModels
{
	public class CartVM
	{
		public IEnumerable<CartItem> ListCart { get;set; }
		public Order Order { get; set; }
		public string PromotionCode { get; set; }
	}
}
