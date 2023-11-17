using ConnectSQL.Data;
using ConnectSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectSQL.Service
{
    public class LoaiResponsitoryInMemory : ILoaiResponsitory
    {
        static List<LoaiVM> loais = new List<LoaiVM>
        {
            new LoaiVM{ MaLoai= 1, TenLoai = "Tivi"},
            new LoaiVM{ MaLoai= 2, TenLoai = "Tủ lạnh"},
            new LoaiVM{ MaLoai= 3, TenLoai = "Điều hòa"},
            new LoaiVM{ MaLoai= 4, TenLoai = "Máy giặt"},
        };
        public LoaiVM Add(LoaiModel loai)
        {
            var loaiNew = new LoaiVM
            {
                MaLoai = loais.Max(lo=> lo.MaLoai)+ 1, 
                TenLoai = loai.TenLoai,
            };
            loais.Add(loaiNew);
            return loaiNew;
        }

        public void Delete(int id)
        {
            var loaiFind = loais.SingleOrDefault(loai => loai.MaLoai == id);
            loais.Remove(loaiFind!);
        }

        public List<LoaiVM> GetAll()
        {
            return loais;
        }

        public LoaiVM GetById(int id)
        {
            return loais.SingleOrDefault((obj)=> obj.MaLoai == id)!;
            
        }

        public void Update(int id, LoaiVM loai)
        {
            var loaiFind = loais.SingleOrDefault(loai => loai.MaLoai == id);
            if (loais != null && id == loai.MaLoai)
            {
                loai.TenLoai = loai.TenLoai;
            }
        }
    }
}
