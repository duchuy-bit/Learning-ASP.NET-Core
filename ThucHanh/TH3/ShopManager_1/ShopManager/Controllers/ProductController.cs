using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManager.DAL;
using ShopManager.Helper;
using ShopManager.Models;

namespace ShopManager.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL productDAL = new ProductDAL();
        CategoryDAL categoryDAL = new CategoryDAL();

        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = productDAL.getAll();

            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            // Lấy Thông tin Product từ Id
            Product product = new Product();
            product = productDAL.GetProductById(id);

            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            // Khai báo Model Category
            List<Category> categories = new List<Category>();

            // Lấy danh sách Category từ DataBase
            categories = categoryDAL.getAll();

            // Khai báo Model
            ProductForm productAddNew = new ProductForm();

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

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductForm productAddNew, IFormFile Img)
        {
            try
            {
                //tự động lấy thời gian CreateAt và UpdateAt theo giờ hệ thống
                DateTime now = DateTime.Now;
                productAddNew.CreateAt = now;
                productAddNew.UpdateAt = now;

                //Upload Hinh
                var ImageName = ImageHelper.UpLoadImage(Img, "SanPham");
                productAddNew.Img = ImageName;

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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            // Lấy Thông tin Product từ Id
            Product product = new Product();
            product = productDAL.GetProductById(id);

            //Khai báo Model ProductForm
            ProductForm productForm = new ProductForm();
            productForm.Id = id;
            productForm.Title = product.Title;
            productForm.Content = product.Content;
            productForm.Img = product.Img;
            productForm.Price = product.Price;
            productForm.CreateAt = product.CreateAt;
            productForm.UpdateAt = product.UpdateAt;
            productForm.Discount = product.Discount;
            productForm.CategoryId = product.CategoryId;
            productForm.CategoryTitle = product.CategoryTitle;

            //lấy danh sách Category Từ DB
            List<Category> categories = new List<Category>();
            categories = categoryDAL.getAll();

            // Tạo danh sách Input Select
            productForm.ListCategory = new List<SelectListItem>();
            foreach (var item in categories)
            {
                productForm.ListCategory.Add(
                    new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    }
                );
            }

            return View(productForm);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductForm productEdit, IFormFile ImageUpload)
        {
            try
            {
                //tự động lấy thời gian UpdateAt theo giờ hệ thống
                DateTime now = DateTime.Now;
                productEdit.UpdateAt = now;

                //Nếu có hình ảnh được Upload
                if(ImageUpload != null)
                {
                    //Upload Hinh
                    var ImageName = ImageHelper.UpLoadImage(ImageUpload, "SanPham");
                    productEdit.Img = ImageName;
                }

                //lấy Id Category
                int IdCategory = Convert.ToInt32(productEdit.IdCategorySelected);
                productEdit.CategoryId = IdCategory;

                // truy vấn tới CSDL
                bool isSuccess = productDAL.UpdateProduct(productEdit, id);

                // Kiểm tra truy vấn SQL thành công hay không?
                if (isSuccess)
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            // Lấy Thông tin Product từ Id
            Product product = new Product();
            product = productDAL.GetProductById(id);
            return View();
        }

        // POST: ProductController/Delete/5
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
