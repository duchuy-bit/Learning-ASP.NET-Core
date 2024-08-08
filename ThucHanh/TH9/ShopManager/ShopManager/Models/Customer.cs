using System.ComponentModel.DataAnnotations;

namespace ShopManager.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string LastName { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "*")]
        [MaxLength(15, ErrorMessage = "Tối đa 15 kí tự")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Email { get; set; }
        public string Img { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateOnly? DateOfBirth { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string Password { get; set; }
        public string RandomKey { get; set; }
        public DateTime RegisterAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int IsActive { get; set; }
        public int Role { get; set; }
    }
    public class CustomerSignIn
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string Password { get; set; }
    }

    public class CustomerForgotPassword
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Email { get; set; }

        public string RandomCode { get; set; }

        [Display(Name = "OTP")]
        [Required(ErrorMessage = "*")]
        [MaxLength(10, ErrorMessage = "Tối đa 10 kí tự")]
        public string OTP { get; set; }
    }

    public class CustomerNewPassword
    {
        public string Email { get; set; }
        public string RandomKey { set; get; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string NewPassWord { set; get; }

        [Display(Name = "Nhập lại mật khẩu mới")]
        [Required(ErrorMessage = "*")]
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        public string Confirm_NewPassWord { set; get; }
    }
}
