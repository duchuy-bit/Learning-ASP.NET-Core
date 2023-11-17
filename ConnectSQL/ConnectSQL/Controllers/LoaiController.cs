using ConnectSQL.Models;
using ConnectSQL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiResponsitory _loaiaResponsitory;

        public LoaiController(ILoaiResponsitory loaiaResponsitory)
        {
            _loaiaResponsitory = loaiaResponsitory;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_loaiaResponsitory.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        
        [HttpGet("{id}")]
        public IActionResult GetAllById(int id)
        {
            try
            {
                var data = _loaiaResponsitory.GetById(id);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        public IActionResult AddNew(LoaiModel loaiNew)
        {
            try
            {
                return Ok(_loaiaResponsitory.Add(loaiNew));
            }
            catch 
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiVM loai)
        {
            try
            {
                _loaiaResponsitory.Update(id,loai);
                return Ok(loai);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _loaiaResponsitory.Delete(id);
                return Ok(_loaiaResponsitory.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
