using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        ProductDAL productDAL = new ProductDAL();
        public IViewComponentResult Invoke(int? limit)
        {
            int limitProduct = limit ?? 4;

            List<Product> featuredProducts = new List<Product>();

            featuredProducts = productDAL.GetFeaturedProducts(limitProduct);

            return View("FeatureProduct", featuredProducts);
        }

    }
}
