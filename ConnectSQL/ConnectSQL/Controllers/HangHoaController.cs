using ConnectSQL.Data;
using ConnectSQL.Models;
using ConnectSQL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaResponsitory _hangHoaResponsitory;

        public HangHoaController(IHangHoaResponsitory hangHoaResponsitory) {
            _hangHoaResponsitory = hangHoaResponsitory;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            System.Diagnostics.Debug.WriteLine("Get All");
            return Ok(new
            {
                Success = true,
                Data = _hangHoaResponsitory?.GetAll() 
            }) ;
        }

        [HttpPost]
        public IActionResult AddNew(HangHoaVM hanghoaNew)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Add New");
                _hangHoaResponsitory.Add(hanghoaNew);

                return Ok(new
                {
                    Success = true,
                    Data = hanghoaNew
                });
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        //public static List<HangHoa> hangHoas = new();

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(new
        //    {
        //        Success = true,
        //        Data = hangHoas
        //    });
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetWithId(string idFind)
        //{
        //    try
        //    {
        //        var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(idFind));

        //        if (hanghoa == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(new
        //        {
        //            Success = true,
        //            Data = hanghoa
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpPost]
        //public IActionResult Post(HangHoaVM hanghoaNew)
        //{
        //    var hangHoa = new HangHoa
        //    {
        //        MaHangHoa = Guid.NewGuid(),
        //        TenHangHoa = hanghoaNew.TenHangHoa,
        //        Dongia = hanghoaNew.Dongia,
        //    };

        //    hangHoas.Add(hangHoa);
        //    return Ok(new
        //    {
        //        Success = true,
        //        Data = hangHoas
        //    });
        //}

        //[HttpPut]
        //public IActionResult Update(Guid idNew, HangHoaVM hanghoaNew)
        //{
        //    int index = hangHoas.FindIndex((obj) => obj.MaHangHoa == idNew);

        //    hangHoas[index] = new HangHoa
        //    {
        //        MaHangHoa = idNew,
        //        Dongia = hanghoaNew.Dongia,
        //        TenHangHoa = hanghoaNew.TenHangHoa,
        //    };


        //    return Ok(new
        //    {
        //        Success = true,
        //        Data = hangHoas
        //    });
        //}


        //[HttpDelete]
        //public IActionResult Delete(Guid idDelete)
        //{
        //    int index = hangHoas.FindIndex((obj) => obj.MaHangHoa == idDelete);

        //    hangHoas.RemoveAt(index);

        //    return Ok(new
        //    {
        //        Success = true,
        //        Data = hangHoas
        //    });
        //}
    }
}
