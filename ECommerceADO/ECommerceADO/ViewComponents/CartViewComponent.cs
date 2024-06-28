using ECommerceADO.Helper;
using ECommerceADO.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceADO.ViewComponents
{
    
    public class CartViewComponent: ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var cart  = HttpContext.Session.Get<List<CartItem>>(MyConst.CART_KEY)
            ?? new List<CartItem>();

            return View( new CartModel()
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p=>p.ThanhTien)
            });
        }

    }
}
