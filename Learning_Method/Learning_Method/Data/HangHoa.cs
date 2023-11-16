using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Method.Data
{
    [Table("Hanghoa")]
    public class HangHoa
    {
        [Key]
        public Guid Mahh { get; set; }

        [Required]
        [MaxLength(100)]
        public string Tenhh { get; set; }

        public string Mota { get; set; }

        [Range(0, double.MaxValue)]
        public double Dongia { get; set; }
        public byte Giamgia { get; set; }
    }
}
