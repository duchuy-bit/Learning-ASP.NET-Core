using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class CustomerAdminController : Controller
    {
        CustomerAdminDAL customerDAL = new CustomerAdminDAL();

        // GET: CustomerAdminController
        public ActionResult Index()
        {
            List<CustomerAdmin> customerAdmins = new List<CustomerAdmin>();
            customerAdmins = customerDAL.GetAll();
            return View(customerAdmins);
        }

        // GET: CustomerAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
