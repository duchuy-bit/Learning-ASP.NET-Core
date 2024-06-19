using CRUDWithADO.Net.DAL;
using CRUDWithADO.Net.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithADO.Net.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();


            //System.Diagnostics.Debug.WriteLine("ALO");
            //Console.WriteLine("hjejejejje");
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }

            return View(employees);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            return View(employee);
        }
    }
}
