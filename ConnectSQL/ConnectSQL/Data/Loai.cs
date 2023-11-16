using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectSQL.Data
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }

        [Required]
        public string TenLoai { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }

    }
}
