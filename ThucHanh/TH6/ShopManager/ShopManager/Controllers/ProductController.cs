using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL productDAL = new ProductDAL();

        //View List Product
        public IActionResult Index(int? idCategory)
        {
            List<Product> products = new List<Product>();
            products = productDAL.GetListProduct(idCategory);

            return View(products);
        }

        //View Detail
        public IActionResult Detail(int id)
        {
            Product product = new Product();

            product = productDAL.GetProductById(id);

            return View(product);
        }

    }
}
