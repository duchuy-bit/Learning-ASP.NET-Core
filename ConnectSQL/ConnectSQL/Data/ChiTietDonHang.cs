namespace ConnectSQL.Data
{
    public class ChiTietDonHang
    {
        public Guid MaHH { get; set; }
        public Guid MaDh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }


        // Relationship
        public DonHang DonHang { get; set; }   
        public HangHoa HangHoa { get; set; }    
    }
}
