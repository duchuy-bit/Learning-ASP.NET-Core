using ConnectSQL.Data;
using ConnectSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoais = _context.Loais.ToArray();

            return Ok(dsLoais);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault((obj) => obj.MaLoai == id);
            if (loai != null)
            {
                return Ok(loai);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult AddNew(LoaiModel loaiNew)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = loaiNew.TenLoai,
                };
                _context.Add(loai);
                _context.SaveChanges();

                return Ok(loai);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id, LoaiModel loaiNew)
        {
            try
            {
                var loai = _context.Loais.SingleOrDefault((obj) => obj.MaLoai == id);
                if (loai == null)
                {
                    return NotFound();
                }
                loai.TenLoai = loaiNew.TenLoai;
                _context.SaveChanges();
                return Ok(loai);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteLoaiById(int id)
        //{
        //    try
        //    {
        //        //var loai = _context.Loais.SingleOrDefault((obj) => obj.MaLoai == id);
        //        var loai = _context.Loais.Find((obj)=> obj.);

                
        //        _context.SaveChanges();
        //        return Ok(loai);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}
    }
}
