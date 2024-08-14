namespace ShopManager.Areas.Admin.Models
{
    public class PaymentDetailAdmin
    {
        public int PaymentId { get; set; }
        public int ProductId { get; set; }
        public string TitleProduct { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int TotalDetail { get; set; }
    }
}
