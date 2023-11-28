using System.ComponentModel.DataAnnotations;

namespace WebData.Areas.Admin.Models
{
    public class ChangeMKViewModel
    {
        [Key]
        public int AccountID { get; set; }
        [Display(Name ="Mật khẩu hiển tại")]
        public required string PasswordNow { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(5, ErrorMessage = "Bạn cần đặt tối thiểu 5 ký tự")]
        public required string Password { get; set; }

        [Display(Name ="Bạn cần nhập mật khảu mới")]
        [Compare("Pasword", ErrorMessage ="Nhập lại mật khẩu không giống nhau")]
        public required string ConfỉmPassword { get; set; }

    }
}
