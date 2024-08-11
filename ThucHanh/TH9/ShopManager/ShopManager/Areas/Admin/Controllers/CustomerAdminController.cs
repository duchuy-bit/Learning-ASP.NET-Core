using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;
using ShopManager.DAL;
using ShopManager.Helper;
using ShopManager.Models;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class CustomerAdminController : Controller
    {
        CustomerAdminDAL customerDAL = new CustomerAdminDAL();

        // GET: CustomerAdminController
        public ActionResult Index(int? page = 1, string? filter = "", string? search = "", string? sortOrder = "")
        {
            //Khai báo số lượng Rows trong 1 trang
            int pageSize = 5;

            //Text hiển thị trong input sau khi tìm kiếm
            ViewData["CurrentSearch"] = search;

            //Text hiển thị trong input sau khi tìm kiếm
            ViewData["CurrentFilter"] = filter;

            // Lưu Column được sắp xếp
            ViewData["SortColumn"] = sortOrder;

            //Value trả về trong button Sắp xếp
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewData["LastNameSortParm"] = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewData["RegisteredAtSortParm"] = sortOrder == "registeredAt" ? "registeredAt_desc" : "registeredAt";

            //Lấy danh sách Customer sau khi lọc , tìm kiếm, phân trang
            List<CustomerAdmin> customerAdmins = new List<CustomerAdmin>();
            customerAdmins = customerDAL.GetCustomer_Pagination(page, pageSize, filter, search, sortOrder);

            //Lấy tổng số Rows đếm được sau khi lọc , tìm kiếm, phân trang
            int numRows = customerDAL.getCountRowCustomer_Pagination(filter, search);

            //Tính số lượng trang sẽ có (làm tròn lên, vd: 4.3 -> 5)
            double pageCount = (double)numRows / pageSize;
            int maxPage = (int)Math.Ceiling(pageCount);

            //Khai báo Model hiển thị
            CustomerAdmin_Pagination model = new CustomerAdmin_Pagination();
            model.CustomerAdmins = customerAdmins;
            model.CurrentPageIndex = page ?? 1;
            model.PageCount = maxPage;

            return View(model);
        }

        // GET: CustomerAdminController/Details/5
        public ActionResult Details(int id)
        {
            CustomerAdmin? customer = customerDAL.GetCustomerById(id);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Account Not Found";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CustomerAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAdmin customerAdd, IFormFile avatarUpload)
        {
            try
            {
                //Kiểm tra Email đã được đăng ký hay chưa
                CustomerAdmin? customerExist = customerDAL.getCustomerByEmail(customerAdd.Email);
                if (customerExist != null)
                {
                    TempData["ErrorMessage"] = "Email has been registered";
                    return View();
                }

                //Tự động lấy thời gian RegisterAt
                DateTime now = DateTime.Now;
                customerAdd.RegisterAt = now;

                //Avatar
                if (avatarUpload == null)
                {
                    customerAdd.Img = "avatar-default.jpg";
                }
                else
                {
                    var ImageName = ImageHelper.UpLoadImage(avatarUpload, "KhachHang");
                    customerAdd.Img = ImageName;
                }

                //HashPassword
                customerAdd.RandomKey = PasswordHelper.GenerateRandomKey();
                customerAdd.Password = customerAdd.Password.ToMd5Hash(customerAdd.RandomKey);

                bool isSuccess = customerDAL.AddNewCustomer(customerAdd);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Add Account Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Add Account Fail";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: CustomerAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerAdmin? customer = customerDAL.GetCustomerById(id);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Account Not Found";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: CustomerAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerAdmin customer, IFormFile avatarUpload)
        {
            try
            {
                //Tự động lấy thời gian RegisterAt
                DateTime now = DateTime.Now;
                customer.UpdateAt = now;

                //Avatar
                if (avatarUpload != null)
                {
                    var ImageName = ImageHelper.UpLoadImage(avatarUpload, "KhachHang");
                    customer.Img = ImageName;
                }

                bool isSuccess = customerDAL.UpdateCustomerById(id, customer);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Update Account Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Update Account Fail";
                    return View();
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: CustomerAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            CustomerAdmin? customer = customerDAL.GetCustomerById(id);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Account Not Found";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // POST: CustomerAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool isSuccess = customerDAL.DeleteCustomerById(id);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Delete Account Success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Delete Account Fail";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
