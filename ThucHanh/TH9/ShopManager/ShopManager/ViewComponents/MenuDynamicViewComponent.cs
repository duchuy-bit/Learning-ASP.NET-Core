using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using ShopManager.DAL;
using ShopManager.Models;
using System.Security.Claims;

namespace ShopManager.ViewComponents
{
    public class MenuDynamicViewComponent : ViewComponent
    {
        MenuDAL menuDAL = new MenuDAL();

        public IViewComponentResult Invoke()
        {
            //Truy cập Role của Customer đang đăng nhâp 
            string RoleCustomer = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            List<MenuItem> listMenu = new List<MenuItem>();

            List<NavbarItem> navBar = new List<NavbarItem>();

            listMenu = menuDAL.GetAllMenu();

            foreach (var item in listMenu)
            {
                //Nếu Không được phân quyền để truy cập trang Admin
                // Và đường dẫn Url của Menu chứa Area Admin
                if (RoleCustomer != "Administrator" && item.MenuUrl != null && item.MenuUrl.Contains("Admin"))
                {
                    //Bỏ qua Menu này
                }
                else
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
            }
            Console.WriteLine(navBar);
            return View("MenuDynamic", navBar);
        }
    }
}
