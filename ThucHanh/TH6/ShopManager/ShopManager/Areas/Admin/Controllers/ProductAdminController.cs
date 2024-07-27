using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManager.Areas.Admin.DAL;
using ShopManager.Areas.Admin.Models;
using ShopManager.Helper;

namespace ShopManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ProductAdminController : Controller
    {
        ProductAdminDAL productDAL = new ProductAdminDAL();
        CategoryAdminDAL categoryDAL = new CategoryAdminDAL();


        // GET: ProductAdminController
        public ActionResult Index()
        {
            List<ProductAdmin> products = new List<ProductAdmin>();
            products = productDAL.getAll();

            return View(products);
        }


        // GET: ProductAdminController/Details/5
        public ActionResult Details(int id)
        {
            // Lấy Thông tin Product từ Id
            ProductAdmin product = new ProductAdmin();
            product = productDAL.GetProductById(id);

            return View(product);

        }

        // GET: ProductAdminController/Create
        public ActionResult Create()
        {
            // Khai báo Model Category
            List<CategoryAdmin> categories = new List<CategoryAdmin>();

            // Lấy danh sách Category từ DataBase
            categories = categoryDAL.getAll();

            // Khai báo Model
            ProductFormAdmin productAddNew = new ProductFormAdmin();

            // Tạo danh sách Input Select
            productAddNew.ListCategory = new List<SelectListItem>();
            foreach (var item in categories)
            {
                productAddNew.ListCategory.Add(
                    new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    }
                );
            }
            return View(productAddNew);
        }

        // POST: ProductAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormAdmin productAddNew, IFormFile Img)
        {
            try
            {
                //tự động lấy thời gian CreateAt và UpdateAt theo giờ hệ thống
                DateTime now = DateTime.Now;
                productAddNew.CreateAt = now;
                productAddNew.UpdateAt = now;

                //Upload Hinh
                if (Img == null)
                {
                    productAddNew.Img = "";
                }
                else
                {
                    var ImageName = ImageHelper.UpLoadImage(Img, "SanPham");
                    productAddNew.Img = ImageName;
                }
                

                //lấy Id Category
                int IdCategory = Convert.ToInt32(productAddNew.IdCategorySelected);
                productAddNew.CategoryId = IdCategory;

                // truy vấn tới CSDL
                bool IsInserted = productDAL.AddNew(productAddNew);

                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsInserted)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Insert Product Success");
                    TempData["SuccessMessage"] = "Insert Success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Insert Product Fail");
                    TempData["ErrorMessage"] = "Insert Fail";
                    return View();
                }
            }
            catch (Exception ex)
            {
                //Error
                Console.WriteLine("Insert Product error ", ex.Message);
                return View();
            }
        }

        // GET: ProductAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            // Lấy Thông tin Product từ Id
            ProductAdmin product = new ProductAdmin();
            product = productDAL.GetProductById(id);

            //Khai báo Model ProductFormAdmin
            ProductFormAdmin ProductFormAdmin = new ProductFormAdmin();
            ProductFormAdmin.Id = id;
            ProductFormAdmin.Title = product.Title;
            ProductFormAdmin.Content = product.Content;
            ProductFormAdmin.Img = product.Img;
            ProductFormAdmin.Price = product.Price;
            ProductFormAdmin.CreateAt = product.CreateAt;
            ProductFormAdmin.UpdateAt = product.UpdateAt;
            ProductFormAdmin.Rate = product.Rate;
            ProductFormAdmin.CategoryId = product.CategoryId;
            ProductFormAdmin.CategoryTitle = product.CategoryTitle;

            //lấy danh sách Category Từ DB
            List<CategoryAdmin> categories = new List<CategoryAdmin>();
            categories = categoryDAL.getAll();

            // Tạo danh sách Input Select
            ProductFormAdmin.ListCategory = new List<SelectListItem>();
            foreach (var item in categories)
            {
                ProductFormAdmin.ListCategory.Add(
                    new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    }
                );
            }
            return View(ProductFormAdmin);
        }

        // POST: ProductAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductFormAdmin productEdit, IFormFile ImageUpload)
        {
            try
            {
                //tự động lấy thời gian UpdateAt theo giờ hệ thống
                DateTime now = DateTime.Now;
                productEdit.UpdateAt = now;

                //Nếu có hình ảnh được Upload
                if (ImageUpload != null)
                {
                    //Upload Hinh
                    var ImageName = ImageHelper.UpLoadImage(ImageUpload, "SanPham");
                    productEdit.Img = ImageName;
                }

                //lấy Id Category
                int IdCategory = Convert.ToInt32(productEdit.IdCategorySelected);
                productEdit.CategoryId = IdCategory;

                // truy vấn tới CSDL
                bool IsInserted = productDAL.UpdateProduct(productEdit, id);

                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsInserted)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Update Product Success");
                    TempData["SuccessMessage"] = "Update Success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Update Product Fail");
                    TempData["ErrorMessage"] = "Update Fail";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: ProductAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            // Lấy Thông tin Product từ Id
            ProductAdmin product = new ProductAdmin();
            product = productDAL.GetProductById(id);
            return View();
        }

        // POST: ProductAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //Truy vấn SQL
                var IsSuccess = productDAL.DeleteProduct(id);

                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsSuccess)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Delete Product Success");
                    TempData["SuccessMessage"] = "Update Success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Delete Product Fail");
                    TempData["ErrorMessage"] = "Update Fail";
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
