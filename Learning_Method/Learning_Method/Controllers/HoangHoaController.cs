using Learning_Method.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Method.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {

        public static List<HangHoa> hangHoas = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Success = true,
                Data = hangHoas
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetWithId(string idFind)
        {
            try
            {
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(idFind));

                if (hanghoa == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Success = true,
                    Data = hanghoa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Post(HangHoaVM hanghoaNew)
        {
            var hangHoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hanghoaNew.TenHangHoa,
                Dongia = hanghoaNew.Dongia,
            };

            hangHoas.Add(hangHoa);
            return Ok(new
            {
                Success = true,
                Data = hangHoas
            });
        }

        [HttpPut]
        public IActionResult Update(Guid idNew, HangHoaVM hanghoaNew)
        {
            int index = hangHoas.FindIndex((obj) => obj.MaHangHoa == idNew);

            hangHoas[index] = new HangHoa
            {
                MaHangHoa = idNew,
                Dongia = hanghoaNew.Dongia,
                TenHangHoa = hanghoaNew.TenHangHoa,
            };


            return Ok(new
            {
                Success = true,
                Data = hangHoas
            });
        }


        [HttpDelete]
        public IActionResult Delete(Guid idDelete)
        {
            int index = hangHoas.FindIndex((obj) => obj.MaHangHoa == idDelete);

            hangHoas.RemoveAt(index);

            return Ok(new
            {
                Success = true,
                Data = hangHoas
            });
        }
    }
}
