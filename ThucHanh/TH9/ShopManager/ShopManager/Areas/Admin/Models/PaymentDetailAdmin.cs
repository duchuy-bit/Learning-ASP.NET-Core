namespace ShopManager.Areas.Admin.Models
{
    public class PaymentDetailAdmin
    {
        public int ProductId { get; set; }
        public int PaymentId { get; set; }
        public int Price { get; set; }
        public int Quantiry { get; set; }
        public int Total { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
