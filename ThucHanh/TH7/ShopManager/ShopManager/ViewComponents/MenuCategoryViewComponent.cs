using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        CategoryDAL categoryDAL = new CategoryDAL();

        public IViewComponentResult Invoke()
        {
            List<CategoryMenu> categoryMenus = new List<CategoryMenu>();

            categoryMenus = categoryDAL.getAllWithCount();
            return View("Default", categoryMenus);
        }
    }
}
