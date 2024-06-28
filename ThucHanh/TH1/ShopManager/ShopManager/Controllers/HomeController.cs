using Microsoft.AspNetCore.Mvc;
using ShopManager.Models;
using System.Diagnostics;

namespace ShopManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ThucHanh1> thucHanh1s = new List<ThucHanh1> {
                new ThucHanh1()
                {
                    Id = 1,
                    Name = "Thực hành buổi 1",
                    Description = "Cài đặt Visual Studio, Build dự án Hello World"

                },
                new ThucHanh1()
                {
                    Id = 2,
                    Name ="Thực hành buổi 2",
                    Description="Form, Upload Hình ảnh"
                },
                new ThucHanh1()
                {
                    Id = 3,
                    Name ="Thực hành buổi 3",
                    Description="Kết nối SQL Server, Tạo Area phân tách admin và user, tạo Trang Admin thêm xóa sửa"
                },
                new ThucHanh1()
                {
                    Id = 4,
                    Name ="Thực hành buổi 4",
                    Description="Layout, thêm template HTML, ViewComponent"
                },
                new ThucHanh1()
                {
                    Id = 5,
                    Name ="Thực hành buổi 5",
                    Description="Tạo giao diện User, trang chủ có template"
                },
            };
            return View(thucHanh1s);
        }

        public IActionResult Privacy()
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