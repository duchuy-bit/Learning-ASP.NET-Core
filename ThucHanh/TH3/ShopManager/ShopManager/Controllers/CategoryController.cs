using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopManager.DAL;
using ShopManager.Models;

namespace ShopManager.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDAL categoryDAL = new CategoryDAL();

        // GET: CategoryController
        public ActionResult Index()
        {
            List <Category> categories = new List<Category>();
            categories = categoryDAL.getAll();

            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            Category category = new Category();
            category = categoryDAL.getCategoryById(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category categoryNew)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    DateTime now = DateTime.Now;

                    categoryNew.CreateAt = now;
                    categoryNew.UpdateAt = now;

                    IsInserted = categoryDAL.AddNew(categoryNew);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Insert Success";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Insert Fail";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = new Category();

            category = categoryDAL.getCategoryById(id);

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category categoryNew)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);
                    DateTime now = DateTime.Now;
                    categoryNew.UpdateAt = now;

                    IsInserted = categoryDAL.updateCategoryById(id,categoryNew);
                    if (IsInserted)
                    {
                        Console.WriteLine("Update Success");
                        TempData["SuccessMessage"] = "Insert Success";
                    }
                    else
                    {
                        Console.WriteLine("Update Fail");
                        TempData["ErrorMessage"] = "Insert Fail";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update error "+ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = new Category();
            category = categoryDAL.getCategoryById(id);
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);

                    IsInserted = categoryDAL.deleteCategoryById(id);
                    if (IsInserted)
                    {
                        Console.WriteLine("Delete Success");
                        TempData["SuccessMessage"] = "Delete Success";
                    }
                    else
                    {
                        Console.WriteLine("Delete Fail");
                        TempData["ErrorMessage"] = "Delete Fail";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete error " + ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
