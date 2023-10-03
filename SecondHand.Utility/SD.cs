using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public const string OrderStatusPending = "Chờ xác nhận";
        public const string OrderStatusApproved = "Đã xác nhận";
        public const string OrderStatusInProcess = "Đang giao hàng";
        public const string OrderStatusShipped = "Đã giao hàng";
        public const string OrderStatusCancelled = "Bị huỷ";
        public const string OrderStatusRefunded = "Trả hàng";


        public const string PaymentStatusPending = "Đang chờ";
        public const string PaymentStatusApproved = "Đã thanh toán";
        public const string PaymentStatusCancel = "Bị huỷ";
    
        public static string ConvertToCurrencyFormat(decimal price)
            {
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                string formattedPrice = price.ToString("C", cultureInfo)
                    .Replace(" ", "") // Loại bỏ dấu cách
                    .Replace(".", ","); // Thay thế dấu chấm bằng dấu phẩy
                return formattedPrice;
            }
    }
}
