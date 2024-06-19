using CRUDWithADO.Net.DAL;
using CRUDWithADO.Net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithADO.Net.Controllers
{
    public class Employee2Controller : Controller
    {
        Employee_DAL _dal  = new Employee_DAL();

        // GET: Employee2Controller
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            employees = _dal.GetAll();

            if(employees.Count == 0)
            {
                TempData["InfoMessage"] = "Currently employees not available";
            }
            else
            {
                TempData["InfoMessage"] = "Load employees success";
            }


            return View(employees);
        }

        // GET: Employee2Controller/Details/5
        public ActionResult Details(int id)
        {

            var employee = _dal.getEmployeeById(id);

            return View(employee);
        }

        // GET: Employee2Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee2Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        //public ActionResult Create(IFormCollection collection)
        {
            try
            {
                bool IsInserted = false;

                if (ModelState.IsValid)
                {
                    IsInserted = _dal.InsertEmployee(employee);
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


            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Employee2Controller/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var employee = _dal.getEmployeeById(id);

                if (employee != null)
                {
                    TempData["SuccessMessage"] = "Load Employee Success !!!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Load Employee Error !!!";
                }

                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employee2Controller/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(int id, Employee employee)
        {
            try
            {
                var isUpdate = _dal.UpdateEmployee(id, employee);
                if (isUpdate)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Update Fail";
                    return View();
                }

                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Employee2Controller/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _dal.getEmployeeById(id);
                if (employee !=null)
                {
                    TempData["SuccessMessage"] = "Load Success";
                    return View(employee);
                }
                else
                {
                    TempData["SuccessMessage"] = "Load Fail";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employee2Controller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var isDelete = _dal.DeleteEmployee(id);
                if (isDelete)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["SuccessMessage"] = "Load Fail";
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = ex.Message;
                return View();
            }
        }
    }
}
