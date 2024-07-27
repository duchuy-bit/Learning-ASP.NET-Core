using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.ViewComponents
{
    public class MenuDynamicViewComponent : ViewComponent
    {
        MenuDAL menuDAL = new MenuDAL();

        public IViewComponentResult Invoke()
        {
            List<MenuItem> listMenu = new List<MenuItem>();

            List<NavbarItem> navBar = new List<NavbarItem>();

            listMenu = menuDAL.GetAllMenu();

            foreach (var item in listMenu)
            {
                // Is Nav Bar Item
                if (item.ParentId == null)
                {
                    navBar.Add(
                        new NavbarItem()
                        {
                            Id = item.Id,
                            ParentId = item.ParentId,
                            Title = item.Title,
                            MenuUrl = item.MenuUrl,
                            MenuIndex = item.MenuIndex,
                            isVisible = item.isVisible,
                            subItems = new List<NavbarItem>(),
                        }
                    );
                }
                //Is Dropdown Item
                else
                {
                    //Find Item Parent
                    var navbarParent = navBar.Find(p => p.Id == item.ParentId);

                    // if HasValue
                    if (navbarParent != null)
                    {
                        // Add to List Dropdown Item
                        navbarParent.subItems!.Add(
                            new NavbarItem()
                            {
                                Id = item.Id,
                                ParentId = item.ParentId,
                                Title = item.Title,
                                MenuUrl = item.MenuUrl,
                                MenuIndex = item.MenuIndex,
                                isVisible = item.isVisible,
                                subItems = null
                            }
                        );
                    }
                }
            }
            Console.WriteLine(navBar);
            return View("MenuDynamic", navBar);
        }
    }
}
