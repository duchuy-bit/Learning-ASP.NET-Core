using System.ComponentModel.DataAnnotations;

namespace ECommerceADO.ViewModels
{
    public class HangHoaVM
    {
        [Key]
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MotaNgan { get; set; }
        public string TenLoai { get; set; }
    }

    public class HangHoaCTVM
    {
        [Key]
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public string MotaNgan { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public double DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
}
