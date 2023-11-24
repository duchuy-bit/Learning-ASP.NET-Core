using System.ComponentModel.DataAnnotations;

namespace ConnectSQL.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
