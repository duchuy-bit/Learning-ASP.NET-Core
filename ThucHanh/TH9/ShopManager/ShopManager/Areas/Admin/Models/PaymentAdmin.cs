using System.ComponentModel.DataAnnotations;

namespace ShopManager.Areas.Admin.Models
{
    public class PaymentAdmin
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
        public int Total { get; set; }        
    }
}
