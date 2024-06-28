using ECommerceADO.DAL;
using ECommerceADO.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceADO.ViewComponents
{
    public class MenuLoaiViewComponent: ViewComponent
    {
        MenuLoaiDAL _menuLoaiDAL = new MenuLoaiDAL();

        public IViewComponentResult Invoke(int limit)
        {
            int _limit = limit!= 0 ? limit : 6;

            var listMenuLoai = _menuLoaiDAL.getAll(_limit);

            return View(listMenuLoai); // Default.cshtml
            //return View("Default", data);
        }
    }
}
