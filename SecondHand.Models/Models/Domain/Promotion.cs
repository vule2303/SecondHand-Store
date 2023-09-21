using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SecondHand.Models.Domain { 

public partial class Promotion
    {
    public int Id { get; set; }

        [Display(Name = "Mã")]

        public string Code { get; set; } = null!;

        [Display(Name = "Kiểu giảm giá")]

        public string? DiscountType { get; set; }

        [Display(Name = "Giá trị giảm giá")]

        public decimal? DiscountValue { get; set; }

        [Display(Name = "Giá trị đơn hàng tối thiểu")]

        public int MiniumOrderAmount { get; set; }

        [Display(Name = "Ngày Tạo")]

        public DateTime? Created { get; set; }

        [Display(Name = "Đang hoạt động")]

        public bool? IsActive { get; set; }

        [Display(Name = "Ngày bắt đầu")]


        public DateTime? StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]

        public DateTime? EndDate { get; set; }


        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
}