using ConnectSQL.Data;
using ConnectSQL.Models;

namespace ConnectSQL.Service
{
    public class LoaiResponsitory : ILoaiResponsitory
    {
        private readonly MyDbContext _context;

        public LoaiResponsitory(MyDbContext context) {
            _context = context;
        }
        public LoaiVM Add(LoaiModel loai)
        {
            var loaiNew = new Loai
            {
                TenLoai = loai.TenLoai,
            };
            _context.Add(loaiNew);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = loaiNew.MaLoai,
                TenLoai = loai.TenLoai,
            };
        }

        public void Delete(int id)
        {
            var loais = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            if (loais != null)
            {
                _context.Remove(loais);
                _context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            System.Diagnostics.Debug.WriteLine("LoaiResponsitory Get All");
            var loais = _context.Loais.Select(loai => new LoaiVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
            });

            return loais.ToList();
        }

        public LoaiVM GetById(int id)
        {
            var loais = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            if (loais != null)
            {
                return new LoaiVM
                {
                    MaLoai = loais.MaLoai,
                    TenLoai = loais.TenLoai
                };
            }
            return null;
        }

        public void Update(int id, LoaiVM loai)
        {
            var loais = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            if (loais != null && id == loai.MaLoai )
            {
                loai.TenLoai = loai.TenLoai;
                _context.SaveChanges();
            }
        }
    }
}
