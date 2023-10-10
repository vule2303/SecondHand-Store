using Org.BouncyCastle.Bcpg.OpenPgp;
using SecondHand.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.ViewModels
{
	public class CartVM
	{
		public IEnumerable<CartItem> ListCart { get;set; }
		public Order Order { get; set; }
        [Required(ErrorMessage = "Phải nhập {0}")]
        [DisplayName("Mã giảm giá")]
        public string PromotionCode { get; set; }
	}
}
