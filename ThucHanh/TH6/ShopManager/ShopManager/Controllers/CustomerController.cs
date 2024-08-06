using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Helper;
using ShopManager.Models;
using System.Data;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult Profile()
        {
            string? idTam = "";
            if (HttpContext.User.FindFirstValue("CustomerId") != null)
            {
                idTam = HttpContext.User.FindFirstValue("CustomerId");
            }

            if (idTam == null)
            {
                return RedirectToAction("SignIn");
            }

            int idCustomer = Convert.ToInt32(idTam!);

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

        #region SIGN_UP
        // ------------------ SIGN UP --------------------
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Customer customerSignUp, IFormFile ImgUpload)
        {
            try
            {
                //Kiểm tra Email đã được đăng ký tài khoản hay chưa
                Customer? customerExist = new Customer();
                customerExist = customerDAL.GetCustomerByEmail(customerSignUp.Email);
                if (customerExist != null)
                {
                    TempData["SignUpErrorMessage"] = "Email đã được đăng ký cho tài khoản khác";
                    return View();
                }

                // RegisterAt va UpdateAt được lấy tự động theo giờ hệ thống
                DateTime now = DateTime.Now;

                customerSignUp.RegisterAt = now;
                customerSignUp.UpdateAt = now;

                //Nếu có hình ảnh được Upload
                if (ImgUpload != null)
                {
                    //Upload Hinh
                    var ImageName = ImageHelper.UpLoadImage(ImgUpload, "KhachHang");
                    customerSignUp.Img = ImageName;
                }
                else
                {
                    //Sử dụng avatar mặc định của project
                    customerSignUp.Img = "avatar-default.jpg";
                }

                //kiểm tra Address có null hay không
                if (customerSignUp.Address == null)
                {
                    customerSignUp.Address = "";
                }

                //HashPassword
                customerSignUp.RandomKey = PasswordHelper.GenerateRandomKey();
                customerSignUp.Password = customerSignUp.Password.ToMd5Hash(customerSignUp.RandomKey);

                bool isSuccess = customerDAL.SignUp(customerSignUp);

                // Kiểm tra truy vấn SQL thành công hay không?
                if (isSuccess)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Update Customer Success");
                    TempData["SignInSuccessMessage"] = "Đăng ký thành công";
                    return RedirectToAction("SignIn");
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Update Customer Fail");
                    TempData["SignUpErrorMessage"] = "Lỗi hệ thống";
                    return RedirectToAction("SignUp");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
        #endregion

        #region SIGN_IN
        // ------------------ SIGN IN --------------------
        public IActionResult SignIn(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(CustomerSignIn customerSignIn, string? ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ReturnUrl = ReturnUrl;

                    Customer? customer = new Customer();
                    customer = customerDAL.GetCustomerByEmail(customerSignIn.Email);

                    if (customer == null)
                    {
                        ModelState.AddModelError("Loi", "Không có khách hàng này");
                    }
                    else
                    {
                        if (customer.IsActive == 0)
                        {
                            ModelState.AddModelError("Thông báo", "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ Admin");
                        }
                        else
                        {
                            string hashPassword = customerSignIn.Password.ToMd5Hash(customer.RandomKey);
                            if (customer.Password != hashPassword)
                            {
                                ModelState.AddModelError("Thông báo", "Sai mật khẩu");
                            }
                            else
                            {
                                var claims = new List<Claim>
                            {
                                new Claim("CustomerEmail", customer.Email),
                                new Claim(ClaimTypes.Name, customer.FirstName),
                                new Claim("CustomerFirstName", customer.FirstName),
                                new Claim("CustomerLastName", customer.LastName),
                                new Claim("CustomerId", customer.Id.ToString()),
                                
                                //claim -role động   (Cấp quyền)                              
                                new Claim(ClaimTypes.Role, customer.Role == 1 ? "Administrator" : "Customer"),
                            };

                                var claimIdentity = new ClaimsIdentity(claims, "login");
                                var claimPricipal = new ClaimsPrincipal(claimIdentity);

                                await HttpContext.SignInAsync(claimPricipal);

                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return Redirect("/");
                                }

                            }
                        }
                    }

                    return View();
                }
                else return View();
            }catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return View();
            }
        }
        #endregion

        #region SIGN_OUT
        //------------ SIGN OUT -------------
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion
    }
}
