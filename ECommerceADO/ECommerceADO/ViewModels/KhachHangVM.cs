using System.ComponentModel.DataAnnotations;

namespace ECommerceADO.ViewModels
{
    public class KhachHangVM
    {
        [Display(Name = "Ten Dang Nhap")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string MaKH { get; set; }

        [Display(Name = "Mat khau")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Ho Va Ten")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string HoTen { get; set; }


        [Display(Name = "Địa chỉ")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
        public string DiaChi { get; set; }


        public bool GioiTinh { get; set; } = true;



        [DataType(DataType.Date)]
        [Display(Name = "Ngay Sinh")]
        public DateTime? NgaySinh { get; set; }



        [MaxLength(15, ErrorMessage = "Tối đa 15 kí tự")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Sai định dạng số điện thoại")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Chưa đúng định dạng")]
        public string Email { get; set; }

        public string? Hinh { get; set; }

        public bool HieuLuc { get; set; } = true;

        public int vaiTro { get; set; } = 0;

        public string RandomKey { get; set; }
    }
}
