using Microsoft.AspNetCore.Mvc;

namespace ShopManager.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
