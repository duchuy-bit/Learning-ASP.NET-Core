using ConnectSQL.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectSQL.Models
{
    public class HangHoaVM
    {
        public Guid MaHH { get; set; }
        public string TenHH { get; set; }
        public string Mota { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }

        public int? MaLoai { get; set; }
    }
}
