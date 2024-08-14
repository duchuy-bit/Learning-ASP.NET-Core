using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;
using ShopManager.DAL;
using System.ComponentModel.DataAnnotations;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PaymentAdminController : Controller
    {
        PaymentAdminDAL paymentAdminDAL = new PaymentAdminDAL();
        // GET: PaymentAdminController
        public ActionResult Index(int? page = 1, string? search = "", string? sortOrder = "")
        {
            //Khai báo số lượng Rows trong 1 trang
            int pageSize = 10;

            //Text hiển thị trong input sau khi tìm kiếm
            ViewData["CurrentSearch"] = search;

            // Lưu Column được sắp xếp
            ViewData["SortColumn"] = sortOrder;

            //Value trả về trong button Sắp xếp
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewData["LastNameSortParm"] = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewData["CreateAtSortParm"] = sortOrder == "createAt" ? "createAt_desc" : "createAt";
            ViewData["TotalSortParm"] = sortOrder == "total" ? "total_desc" : "total";

            //Lấy danh sách Customer sau  tìm kiếm, phân trang
            List<PaymentAdmin> paymentAdmins = new List<PaymentAdmin>();
            paymentAdmins = paymentAdminDAL.GetPayment_Pagination(page, pageSize, search, sortOrder);

            //Lấy tổng số Rows đếm được sau  tìm kiếm, phân trang
            int numRows = paymentAdminDAL.getCountRowPayment_Pagination( search);

            //Tính số lượng trang sẽ có (làm tròn lên, vd: 4.3 -> 5)
            double pageCount = (double)numRows / pageSize;
            int maxPage = (int)Math.Ceiling(pageCount);

            //Khai báo Model hiển thị
            PaymentAdmin_Pagination model = new PaymentAdmin_Pagination();
            model.PaymentAdmins = paymentAdmins;
            model.CurrentPageIndex = page ?? 1;
            model.PageCount = maxPage;

            return View(model);
        }

        // GET: PaymentAdminController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                PaymentAdmin? paymentAdmin = paymentAdminDAL.GetPaymentById(id);
                if (paymentAdmin == null)
                {
                    TempData["ErrorMessage"] = "Payment Not Found";
                    return RedirectToAction("Index");
                }

                List<PaymentDetailAdmin> paymentDetailAdmins = paymentAdminDAL.GetPaymentDetail_ByPaymentId(id);

                PaymentAdmin_PaymentDetail model = new PaymentAdmin_PaymentDetail();

                model.Id = id;
                model.FirstName = paymentAdmin.FirstName;
                model.LastName = paymentAdmin.LastName;
                model.Phone = paymentAdmin.Phone;
                model.Email = paymentAdmin.Email;
                model.CreateAt = paymentAdmin.CreateAt;
                model.Total = paymentAdmin.Total;
                model.CustomerId = paymentAdmin.CustomerId;
                model.Avatar = paymentAdmin.Avatar;

                model.PaymentDetailAdmins = paymentDetailAdmins;

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "System Error";
                return RedirectToAction("Index");
            }
        }        
    }
}
