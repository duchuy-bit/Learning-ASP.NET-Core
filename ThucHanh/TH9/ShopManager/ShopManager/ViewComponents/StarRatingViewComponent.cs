using Microsoft.AspNetCore.Mvc;

namespace ShopManager.ViewComponents
{
    public class StarRatingViewComponent:ViewComponent
    {

        public IViewComponentResult Invoke(double star)
        {
            return View("StarRating", star);
        }
    }
}
