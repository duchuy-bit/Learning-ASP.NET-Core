using ECommerceADO.ViewModels;
using Microsoft.AspNetCore.Mvc;

using ECommerceADO.Helper;
using ECommerceADO.DAL;

namespace ECommerceADO.Controllers
{
    public class CartController : Controller
    {
        HangHoaDAL _hangHoaDAL = new HangHoaDAL();



        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MyConst.CART_KEY) ??
            new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity =  1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHH == id);
            if (item  == null)
            {
                HangHoaCTVM ? hangHoaCTVM = new HangHoaCTVM();
                hangHoaCTVM = _hangHoaDAL.GetById(id);
                if (hangHoaCTVM == null)
                {
                    TempData["Message"] = "Khong tim thay san pham";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHH = hangHoaCTVM.MaHH,
                    DonGia = hangHoaCTVM.DonGia ,
                    Hinh = hangHoaCTVM.Hinh,
                    SoLuong = quantity,
                    TenHH = hangHoaCTVM.TenHH,
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MyConst.CART_KEY,gioHang);


            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;

            var item = gioHang.SingleOrDefault(p => p.MaHH == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set( MyConst.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

    }
}
