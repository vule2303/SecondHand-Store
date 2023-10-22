using SecondHand.Models.Domain;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Models.ViewModels
{
    public class ChangePasswordVM
    {
        public ApplicationUser User { get; set; }
        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display (Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0} phải ít nhất {2} đến {1} ký tự", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không đúng")]
        public string ConfirmNewPassword { get; set;}
    }

}
