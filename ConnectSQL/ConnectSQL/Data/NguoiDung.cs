using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectSQL.Data
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

    }
}
