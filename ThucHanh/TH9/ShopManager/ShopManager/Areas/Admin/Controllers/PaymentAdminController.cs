using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PaymentAdminController : Controller
    {
        PaymentAdminDAL paymentAdminDAL = new PaymentAdminDAL();
        // GET: PaymentAdminController
        public ActionResult Index()
        {
            List<PaymentAdmin> paymentAdmins = new List<PaymentAdmin>();
            paymentAdmins = paymentAdminDAL.GetAllPayment();
            return View(paymentAdmins);
        }

        // GET: PaymentAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentAdminController/Create
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

        // GET: PaymentAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentAdminController/Edit/5
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

        // GET: PaymentAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentAdminController/Delete/5
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
