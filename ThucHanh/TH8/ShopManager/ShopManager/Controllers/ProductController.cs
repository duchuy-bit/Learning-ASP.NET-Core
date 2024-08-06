using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL productDAL = new ProductDAL();

        //View List Product
        public IActionResult Index(int? idCategory, int page = 1, string sortOrder = "")
        {
            //Khai báo số lượng Row trong 1 trang
            int pageSize = 5;

            // Lưu Id Category 
            ViewData["IdCategory"] = idCategory;

            // Lưu Column được sắp xếp
            ViewData["SortColumn"] = sortOrder;

            // Lấy danh sách Product sau khi phân trang
            List<Product> products = new List<Product>();
            products = productDAL.GetProducts_Pagination(idCategory, page, pageSize, sortOrder);

            //Lấy tổng số lượng Product
            int rowCount = productDAL.GetListProduct(idCategory).Count();

            //Tính số lượng trang
            double pageCount = (double)rowCount / pageSize;
            int maxPage = (int)Math.Ceiling(pageCount);

            //Tạo model để hiển thị
            ProductPagination model = new ProductPagination();
            model.Products= products;
            model.CurrentPageIndex = page;
            model.PageCount = maxPage;

            return View(model);
        }

        //View Detail
        public IActionResult Detail(int id)
        {
            Product? product = new Product();

            product = productDAL.GetProductById(id);

            return View(product);
        }

    }
}
