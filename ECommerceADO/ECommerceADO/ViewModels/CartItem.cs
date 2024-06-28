using System.ComponentModel.DataAnnotations;

namespace ECommerceADO.ViewModels
{
    public class CartItem
    {
        [Key]
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong{ get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
