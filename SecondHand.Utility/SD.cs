using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Utility
{
    public static class SD
    {
        public const string Role_User_Cust = "Customer";
        public const string Role_Compa = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string ssShopingCart = "Shoping cart Session";

        public static double GetPriceBaseOnQuanity(int quantity, decimal price)
        {         
            return Convert.ToDouble(price * quantity);
        }
    }
}
