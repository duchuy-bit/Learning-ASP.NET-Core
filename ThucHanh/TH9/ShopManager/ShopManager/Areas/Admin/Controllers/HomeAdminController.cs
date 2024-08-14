using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeAdminController : Controller
    {
        PaymentAdminDAL paymentAdminDAL = new PaymentAdminDAL();
        ProductAdminDAL productAdminDAL = new ProductAdminDAL();
        CategoryAdminDAL categoryAdminDAL = new CategoryAdminDAL();
        CustomerAdminDAL customerAdminDAL = new CustomerAdminDAL();

        public IActionResult Index()
        {
            DashboardAdmin dashboardAdmin = new DashboardAdmin();
            int nowYear = DateTime.Now.Year;
            int nowMonth = DateTime.Now.Month;

            //Chart
            dashboardAdmin.RevenueAdmins = paymentAdminDAL.GetTotalRevenue_WithMonth_InYear(nowYear);
            dashboardAdmin.PaymentInMonths = paymentAdminDAL.GetCountPayment_WithMonth_InYear(nowYear);

            //Card

            List<CardDashboard> cardDashboards = new List<CardDashboard>();

            // Payments in Month
            List<PaymentInMonth> paymentInMonths = paymentAdminDAL.GetCountPayment_WithMonth_InYear(nowYear);

            cardDashboards.Add(
                new CardDashboard()
                {
                    Title = "Payments/Month",
                    Count = paymentInMonths.Find(item => item.Month == nowMonth)?.CountPayment ?? 0,
                    ControllerName = "PaymentAdmin",
                    Color = "#007BFF"
                });
            //Products
            cardDashboards.Add(
                new CardDashboard()
                {
                    Title = "Products",
                    Count = productAdminDAL.getAll().Count(),
                    ControllerName = "ProductAdmin",
                    Color= "#FFC108"
                });
            //Categories
            cardDashboards.Add(
                new CardDashboard()
                {
                    Title = "Categories",
                    Count = categoryAdminDAL.getAll().Count(),
                    ControllerName = "CategoryAdmin",
                    Color= "#28A745"
                });
            //Customer
            cardDashboards.Add(
                new CardDashboard()
                {
                    Title = "Accounts",
                    Count = customerAdminDAL.GetAll().Count(),
                    ControllerName = "CustomerAdmin",
                    Color= "#DC3545"
                });

            dashboardAdmin.CardDashboards = cardDashboards;



            return View(dashboardAdmin);
        }
    }
}
