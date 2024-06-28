using ECommerceADO.DAL;
using ECommerceADO.Helper;
using ECommerceADO.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Claims;
using System.Text.Json;

namespace ECommerceADO.Controllers
{
    public class KhachHangController : Controller
    {

        KhachHangDAL _khachHangDAL = new KhachHangDAL();

        #region DangKy

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }


        
        [HttpPost, ActionName("DangKy")]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {

            Console.WriteLine("Dang Ky");

            if (ModelState.IsValid)
            {
                Console.WriteLine("Model "+ model);
                var hinhUpload = "";
                if (Hinh != null)
                {
                    hinhUpload = MyUtil.UpLoadHinh(Hinh, "KhachHang");
                }
                try
                {
                    var isSuccess = _khachHangDAL.DangKy(model, hinhUpload);
                    if (isSuccess)
                    {
                        return RedirectToAction("Index", "HangHoa");
                    }
                    else
                    {
                        Console.WriteLine("riuewurieut");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                

            }
            else
            {
                Console.WriteLine("Sai Dinh Dang "+ JsonSerializer.Serialize(model));
            }

            Console.WriteLine("finish");
            return View();
        }
        #endregion

        #region DangNhap

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                KhachHangVM? userLogin =  _khachHangDAL.DangNhap(model);
                if (userLogin == null)
                {
                    ModelState.AddModelError("Loi", "Khong co khach hang nay");
                }
                else
                {
                    if (!userLogin!.HieuLuc)
                    {
                        ModelState.AddModelError("Loi", "Tai khoan da bi khoa");
                    }
                    else
                    {
                        Console.WriteLine("admin".ToMd5Hash(userLogin.RandomKey));
                        if (userLogin.MatKhau != model.Password.ToMd5Hash(userLogin.RandomKey))
                        {
                            ModelState.AddModelError("Loi", "Sai Mat Khau");
                        }
                        else
                        {
                            //
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, userLogin.Email),
                                new Claim(ClaimTypes.Name, userLogin.HoTen),
                                new Claim("CustomerId", userLogin.MaKH),
                                

                                //claim -role động 
                                new Claim(ClaimTypes.Role, "Customer"),

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
            }

            return View();
        }

        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
