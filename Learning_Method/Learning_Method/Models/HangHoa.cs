namespace Learning_Method.Models
{
    public class HangHoaVM
    {
        public string TenHangHoa { get; set; }
        public int Dongia { get; set; }
    }

    public class HangHoa: HangHoaVM
    {
        public Guid MaHangHoa { get; set; }
    }
}
