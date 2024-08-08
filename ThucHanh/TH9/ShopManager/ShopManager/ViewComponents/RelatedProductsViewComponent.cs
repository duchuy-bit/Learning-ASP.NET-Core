using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        ProductDAL productDAL = new ProductDAL();
        public IViewComponentResult Invoke(int idProduct, int IdCategory, int limit)
        {
            List<Product> relatedProducts = new List<Product>();
            relatedProducts = productDAL.GetRelatedProducts(idProduct, IdCategory, limit);

            return View("RelatedProducts", relatedProducts);
        }

    }
}
