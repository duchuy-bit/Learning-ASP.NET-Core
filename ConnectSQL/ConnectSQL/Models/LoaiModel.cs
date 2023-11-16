using System.ComponentModel.DataAnnotations;

namespace ConnectSQL.Models
{
    public class LoaiModel
    {
        [Required]
        public string TenLoai { get; set; }
    }
}
