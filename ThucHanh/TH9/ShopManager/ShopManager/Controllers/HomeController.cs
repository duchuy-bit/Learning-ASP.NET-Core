using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;
using System.Diagnostics;

namespace ShopManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ProductDAL productDAL = new ProductDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Lấy danh sách Product sau khi phân trang
            List<Product> products = new List<Product>();
            products = productDAL.GetProducts_Search("", 1, 6, "rate_desc");
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/404")]
        public IActionResult PageNotfound()
        {
            return View();
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}