namespace ShopManager.Areas.Admin.Models
{
    public class DashboardAdmin
    {
        public List<CardDashboard> CardDashboards { get; set; }
        public List<RevenueAdmin> RevenueAdmins { get; set; }
        public List<PaymentInMonth> PaymentInMonths { get; set; }
    }

    public class CardDashboard
    {
        public long Count { get; set; }
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string Color { get; set; }
    }

    public class PaymentInMonth
    {
        public int Month { get; set; }
        public long CountPayment { get; set; }
    }
}
