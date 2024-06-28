using ECommerceADO.DAL;
using ECommerceADO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace ECommerceADO.Controllers
{
    public class HangHoaController : Controller
    {

        HangHoaDAL _hangHoaDAL = new HangHoaDAL();

        public IActionResult Index(int? loai)
        {
            List<HangHoaVM> listHangHoa = new List<HangHoaVM>();
            listHangHoa = _hangHoaDAL.getAll(loai); 

            return View(listHangHoa);
        }

        public IActionResult Search(string? search)
        {
            List<HangHoaVM> listHangHoa = new List<HangHoaVM>();
            listHangHoa = _hangHoaDAL.Search(search);

            return View(listHangHoa);
        }


        public IActionResult Detail(int id)
        {
            HangHoaCTVM? hangHoaCTVM = new HangHoaCTVM();
            hangHoaCTVM = _hangHoaDAL.GetById(id);
            if (hangHoaCTVM == null)
            {
                TempData["Message"] = "Khong tim thay san pham";
                return Redirect("/404");
            }
            return View(hangHoaCTVM);
        }

    }
}
