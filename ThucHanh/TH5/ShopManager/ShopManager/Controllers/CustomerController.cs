using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Helper;
using ShopManager.Models;

namespace ShopManager.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDAL customerDAL = new CustomerDAL();
        public IActionResult Index()
        {
            return View();
        }

        // Profile 
        public IActionResult Profile()
        {
            const int idCustomer = 1;

            Customer? customer = new Customer();
            customer = customerDAL.GetCustomerById(idCustomer);

            // Nếu không tìm thấy User thì trả về trang 404 - Not Found
            if (customer == null)
            {
                return Redirect("/404");
            }

            return View(customer);
        }

        // Update Detail Customer  
        [HttpPost]
        public IActionResult UpdateDetailCustomer(Customer customerUpdate, IFormFile ImgUpload)
        {
            //Tự động lấy thời gian updateAt theo giờ hệ thống khi Customer Update thông tin
            DateTime now = DateTime.Now;
            customerUpdate.UpdateAt = now;

            //Nếu có hình ảnh được Upload
            if (ImgUpload != null)
            {
                //Upload Hinh
                var ImageName = ImageHelper.UpLoadImage(ImgUpload, "KhachHang");
                customerUpdate.Img = ImageName;
            }
            else if (customerUpdate.Img == null)
            {
                customerUpdate.Img = "";
            }

            //kiểm tra Address có null hay không
            if (customerUpdate.Address == null)
            {
                customerUpdate.Address = "";
            }

            bool isSuccess = customerDAL.UpdateDetailCustomer(customerUpdate, customerUpdate.Id);
            // Kiểm tra truy vấn SQL thành công hay không?
            if (isSuccess)
            {
                // Truy vấn Thành công
                Console.WriteLine("Update Customer Success");
                TempData["ProfileSuccessMessage"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Profile");
            }
            else
            {
                // Truy vấn Thất bại
                Console.WriteLine("Update Customer Fail");
                TempData["ProfileErrorMessage"] = "Lỗi hệ thống";
                return RedirectToAction("Profile");
            }
        }
    }
}
