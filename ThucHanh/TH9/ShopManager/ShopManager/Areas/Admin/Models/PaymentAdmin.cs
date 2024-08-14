using System.ComponentModel.DataAnnotations;

namespace ShopManager.Areas.Admin.Models
{
    public class PaymentAdmin
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
        public int Total { get; set; }
    }

    public class PaymentAdmin_Pagination
    {
        public List<PaymentAdmin> PaymentAdmins { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }

    public class PaymentAdmin_PaymentDetail : PaymentAdmin
    {
        public List<PaymentDetailAdmin> PaymentDetailAdmins { get; set; }
    }
}
