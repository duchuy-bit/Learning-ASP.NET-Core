using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MenuAdminController : Controller
    {
        MenuAdminDAL menuAdminDAL = new MenuAdminDAL();
        // GET: MenuAdminController
        public ActionResult Index()
        {
            List<MenuAdmin> menuAdmins = new List<MenuAdmin>();

            menuAdmins = menuAdminDAL.GetAllMenu();

            List<MenuAdmin> model = new List<MenuAdmin>();


            // Lấy tất cả Menu (không phải là menu con)
            foreach (var item in menuAdmins)
            {
                if (item.ParentId == null)
                {
                    item.SubItems = new List<MenuAdmin>();
                    model.Add(item);
                }
            }

            //Lấy tất cả Menu Con
            foreach (var item in menuAdmins)
            {
                if (item.ParentId != null)
                {
                    var parentItem = model.Find(p => p.Id == item.ParentId);
                    parentItem?.SubItems?.Add(item);
                }
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult UpdateVisible(int idMenu, bool value)
        {
            try
            {
                bool isSuccess = menuAdminDAL.updateMenuVisible(idMenu, value);
                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "System Error";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "System Error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateMenuIndex(int idMenu, bool isMoveUp)
        {     
            List<MenuAdmin> menuAdmins = menuAdminDAL.GetAllMenu();

            MenuAdmin itemMenuAdmin = menuAdminDAL.GetMenuAdminById(idMenu);

            //Kiểm tra là menu Con ?
            //Menu con thì có ParentId và ngược lại
            bool isSubMenu = itemMenuAdmin.ParentId != null;

            List<MenuAdmin> filteredMenuAdmins;

            if (isSubMenu)
            {
                // Lọc các menu con của cùng một parent
                filteredMenuAdmins = menuAdmins
                    .Where(item => item.ParentId == itemMenuAdmin.ParentId)
                    .ToList();
            }
            else
            {
                // Lọc các menu chính (có ParentId == null)
                filteredMenuAdmins = menuAdmins
                    .Where(item => item.ParentId == null)
                    .ToList();
            }

            MenuAdmin? targetMenuAdmin;
            if (isMoveUp)
            {
                // Lấy menu có MenuIndex lớn nhất nhưng nhỏ hơn MenuIndex của itemMenuAdmin
                targetMenuAdmin = filteredMenuAdmins
                    .Where(item => item.MenuIndex < itemMenuAdmin.MenuIndex)
                    .OrderByDescending(item => item.MenuIndex)
                    .FirstOrDefault();
            }
            else
            {
                // Lấy menu có MenuIndex lớn nhất nhưng nhỏ hơn MenuIndex của itemMenuAdmin
                targetMenuAdmin = filteredMenuAdmins!
                    .Where(item => item!.MenuIndex > itemMenuAdmin.MenuIndex)
                    .OrderBy(item => item.MenuIndex)
                    .FirstOrDefault();
            }

            //neeus khoong timf thaays targetMenuAdmin
            if (targetMenuAdmin == null)
            {
                TempData["ErrorMessage"] = "System Error";
                return RedirectToAction("Index");
            }

            //Update MenuIndex của idMenu được truyền vào
            bool isSuccessIdMenu = menuAdminDAL.updateMenuIndex(idMenu, targetMenuAdmin.MenuIndex);
            if (!isSuccessIdMenu)
            {
                TempData["ErrorMessage"] = "System Error";
                return RedirectToAction("Index");
            }
            //Update MenuIndex của Menu sau khi tìm targetMenuAdmin
            bool isSuccessMenuFind = menuAdminDAL.updateMenuIndex(targetMenuAdmin.Id, itemMenuAdmin.MenuIndex);
            if (!isSuccessMenuFind)
            {
                TempData["ErrorMessage"] = "System Error";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: MenuAdminController/Details/5
        public ActionResult Details(int id)
        {
            MenuAdmin menuAdmin = menuAdminDAL.GetMenuAdminById(id);
            return View(menuAdmin);
        }

        // GET: MenuAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuAdmin menuNew)
        {
            try
            {
                List<MenuAdmin> menuAdmins = menuAdminDAL.GetAllMenu();
                int maxIndex = menuAdmins.Max(item => item.MenuIndex);

                menuNew.MenuIndex = maxIndex + 1;
                bool isSuccess = menuAdminDAL.AddNewMenu(menuNew, false);

                if (!isSuccess) {
                    TempData["ErrorMessage"] = "System Error";
                    return RedirectToAction("Index");
                }

                TempData["SuccessMessage"] = "Create Menu Success.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: MenuAdminController/Create
        public ActionResult CreateSubMenu(int parentId)
        {
            MenuAdmin menuParent = menuAdminDAL.GetMenuAdminById(parentId);

            MenuAdmin_Parent_SubMenu menuAdmin_Parent_SubMenu = new MenuAdmin_Parent_SubMenu(); 
            menuAdmin_Parent_SubMenu.MenuParent = menuParent;

            return View(menuAdmin_Parent_SubMenu);
        }

        // POST: MenuAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubMenu(MenuAdmin_Parent_SubMenu menuForm)
        {
            try
            {
                List<MenuAdmin> menuAdmins = menuAdminDAL.GetAllMenu();
                int maxIndex = menuAdmins.Max(item => item.MenuIndex);

                menuForm.SubMenu.MenuIndex = maxIndex + 1;
                bool isSuccess = menuAdminDAL.AddNewMenu(menuForm.SubMenu, true);

                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "System Error";
                    return RedirectToAction("Index");
                }

                TempData["SuccessMessage"] = "Create Sub Menu Success.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: MenuAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            MenuAdmin menuAdmin = menuAdminDAL.GetMenuAdminById(id);
            return View(menuAdmin);
        }

        // POST: MenuAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MenuAdmin menuEdit)
        {
            try
            {
                bool isSuccess = menuAdminDAL.UpdateMenuById(id, menuEdit);

                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "System Error";
                    return RedirectToAction("Index");
                }

                TempData["SuccessMessage"] = "Update Menu Success.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            MenuAdmin menuAdmin = menuAdminDAL.GetMenuAdminById(id);
            return View(menuAdmin);
        }

        // POST: MenuAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool isSuccess = menuAdminDAL.DeleteMenuById(id);

                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "System Error";
                    return RedirectToAction("Index");
                }

                TempData["SuccessMessage"] = "Delete Menu Success.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
