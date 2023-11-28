using System.ComponentModel.DataAnnotations;

namespace WebData.Models
{
    public class Login
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [Display(Name = "Tên đăng nhập")]
        public string UserN { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(30, ErrorMessage = "Mật khẩu chỉ tối đa 30 ký tự")]
        public string Passwd { get; set; }
    }
}
